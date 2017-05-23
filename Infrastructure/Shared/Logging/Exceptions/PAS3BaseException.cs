namespace Shared.Logging.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Base class for custom exception
	/// </summary>
	/// <seealso cref="System.Exception" />
	public abstract class PAS3BaseException : Exception
	{
		public PAS3BaseException()
		{
		}

		public PAS3BaseException(string message)
			: base(message)
		{
		}

		public PAS3BaseException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected PAS3BaseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}