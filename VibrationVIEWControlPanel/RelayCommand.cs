﻿using System;
using System.Windows.Input;

namespace VibrationVIEWControlPanel;

public class RelayCommand : ICommand
{
	private readonly Action _execute;

	public RelayCommand(Action execute)
	{
		_execute = execute;
	}

	public bool CanExecute(object? parameter) => true;

	public void Execute(object? parameter) => _execute();

	public event EventHandler? CanExecuteChanged
	{
		add { }
		remove { }
	}
}
