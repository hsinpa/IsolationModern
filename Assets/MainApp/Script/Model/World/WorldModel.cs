using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class WorldModel
{
    private Dictionary<string, int> _StatDict = new Dictionary<string, int>();

    public int FindStatValue(string trait_id)
    {
        if (_StatDict.TryGetValue(trait_id, out int score))
        {
            return score;
        }

        return 0;
    }


    /// <summary>
    /// True => Pass, False => didn't meet requirement
    /// </summary>
    /// <param name="p_raw_constraint"></param>
    /// <returns></returns>
    public bool CheckConstraint(string p_raw_constraint)
    {
        bool isValid = false;

        //if EMPTY == true 
        if (string.IsNullOrEmpty(p_raw_constraint)) return true;

        // string[] p_constraints =  p_raw_constraint.Split('&');
        string strDelimitor = "[&|]";

        string[] p_constraints = Regex.Split(p_raw_constraint, strDelimitor);
        string[] lines = Regex.Matches(p_raw_constraint, strDelimitor).
                                    Cast<Match>()
                                    .Select(m => m.Value)
                                    .ToArray();

        for (int i = 0; i < p_constraints.Length; i++)
        {
            string constraint = p_constraints[i];

            string[] c_varaibles = constraint.Split(' ');
            string trait_id = c_varaibles[0];
            string operator_string = c_varaibles[1];
            int c_value = int.Parse(c_varaibles[2]);

            int m_value = FindStatValue(trait_id);

            isValid = Utility.OperationMethod.AnalyzeStringOperator(operator_string, c_value, m_value);

            Debug.Log("Constraint " + operator_string + ", " + c_value + ", " + m_value);

            //first element and the constraint fail, break loop
            if (i <= 0 && isValid == false)
            {
                break;
            }
            else if (i > 0)
            {
                string logicalSign = lines[i - 1];
                switch (logicalSign)
                {
                    //Break loop if a constraint check fail
                    case "&":
                        if (isValid == false) break;
                        break;

                    //Return true if isvalid passed
                    case "|":
                        if (isValid) return isValid;
                        break;
                }
            }
        }

        return isValid;
    }
}
