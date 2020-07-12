﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;


public class MainAppManager : Singleton<MainAppManager>
{
	protected MainAppManager() { } // guarantee this will be always a singleton only - can't use the constructor!

	private Subject subject;

	public T FindObject<T>(string p_path) where T : Object
	{
		Transform t_view = transform.Find(p_path);
		if (t_view) return t_view.GetComponent<T>();

		return default(T);
	}

	private Observer[] observers = new Observer[0];

	private ModelOrganizer models;

	void Awake()
	{
		//Set up event notificaiton
		subject = new Subject();

		RegisterAllController(subject);

		models = new ModelOrganizer();

		models.Init(StartSimulation);

	}

	private void StartSimulation()
	{
		subject.notify(EventFlag.InGameEvent.Setup, models);
	}

	public T GetObserver<T>() where T : Observer
	{

		foreach (Observer observer in observers)
		{
			if (observer.GetType() == typeof(T)) return (T)observer;
		}

		return default(T);
	}

	public void Notify(string entity, params object[] objects)
	{
		subject.notify(entity, objects);
	}

	void RegisterAllController(Subject p_subject)
	{
		Transform ctrlHolder = transform.Find("controller");
		if (ctrlHolder == null) return;
		observers = ctrlHolder.GetComponentsInChildren<Observer>();

		foreach (Observer observer in observers)
		{
			subject.addObserver(observer);
		}
	}

	public T FindViewObject<T>(string object_path) {
		Transform ctrlHolder = transform.Find("view/"+ object_path);

		return ctrlHolder.GetComponent<T>();
	}
}
