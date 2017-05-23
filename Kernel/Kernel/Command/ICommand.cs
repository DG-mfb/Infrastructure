﻿namespace Kernel
{
	using System;

	/// <summary>
	/// Interface for all commands
	/// </summary>
	public interface ICommand
	{
		Guid Id { get; set; }
	}
}