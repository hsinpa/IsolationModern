using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;


namespace Utility {
    public class OperationMethod
    {
        public static float BasicMathOperator(string p_operator, float p_value, float self_value)
        {
            switch (p_operator)
            {
                case "+=":
                    self_value += p_value;
                    break;
                case "-=":
                    self_value -= p_value;
                    break;

                case "*=":
                    self_value *= p_value;
                    break;

                case "/=":
                    self_value /= p_value;
                    break;

                case "=":
                    self_value = p_value;
                    break;

            }

            return self_value;
        }

        public static bool AnalyzeStringOperator(string p_operator, float p_value, float self_value)
        {
            bool isValid = false;
            switch (p_operator)
            {
                case ">":
                    isValid = self_value > p_value;
                    break;
                case ">=":
                    isValid = self_value >= p_value;
                    break;

                case "<=":
                    isValid = self_value <= p_value;
                    break;

                case "<":
                    isValid = self_value < p_value;
                    break;

                case "==":
                    isValid = self_value == p_value;
                    break;

                case "!=":
                    isValid = self_value != p_value;
                    break;
            }
            return isValid;
        }

    }

}
