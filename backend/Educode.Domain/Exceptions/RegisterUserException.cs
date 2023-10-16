using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Domain.Exceptions
{

	[Serializable]
	public class RegisterUserException : Exception
	{
		public RegisterUserException() { }
		public RegisterUserException(string message) : base(message) { }
		public RegisterUserException(string message, Exception inner) : base(message, inner) { }
		protected RegisterUserException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
