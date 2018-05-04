using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.Assembly
{
	public class Constant<T>
    {
		public static Constant<object> UNDEFINED = new Constant<object>("undefined");
		
		private T value;
		
		public Constant(T value)
		{
			this.value = value;
		}
    }
}
