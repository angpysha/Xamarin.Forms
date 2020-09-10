using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.Core
{
	public static class BindingUtils
	{
		public static void Apply(BindingBase binding, object context,BindableObject bindObj, BindableProperty property, bool fromBindingContextChanged = false)
		{
			binding.Apply(context,bindObj,property,fromBindingContextChanged);
		}

		public static void Unapply(BindingBase binding, bool fromBindingContextChanged = false)
		{
			binding.Unapply(fromBindingContextChanged);
		}
	}
}
