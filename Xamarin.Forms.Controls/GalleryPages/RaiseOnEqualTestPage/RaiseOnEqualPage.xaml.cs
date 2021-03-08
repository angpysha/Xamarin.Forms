using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Controls.GalleryPages.RaiseOnEqualTestPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RaiseOnEqualPage : ContentPage
	{
		public static readonly BindableProperty TestTextProperty = BindableProperty.Create(nameof(TestText),
			true,
			typeof(string),
			typeof(RaiseOnEqualPage),
			default,
			propertyChanged:OnTextPropertyChaned);

		static void OnTextPropertyChaned(BindableObject bindable, object oldvalue, object newvalue)
		{
			var page = bindable as RaiseOnEqualPage;
			page.AppliedCount += 1;
		}

		public static readonly BindablePropertyKey TestTextReadonlyPropertyKey = BindableProperty.CreateReadOnly(
			nameof(TestTextReadonly),
			true,
			typeof(string),
			typeof(RaiseOnEqualPage),
			default,
			propertyChanged: TestTextReadonlyPropertyChanged);

		static void TestTextReadonlyPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var page = bindable as RaiseOnEqualPage;
			page.ReadonlyAppliedCount += 1;
		}

		public static BindableProperty TestTextReadonlyProperty =>
			TestTextReadonlyPropertyKey.BindableProperty;

		public string TestTextReadonly
		{
			get => (string)GetValue(TestTextReadonlyProperty);
			internal set => SetValue(TestTextReadonlyPropertyKey, value);
		}

		public string TestText
		{
			get => (string)GetValue(TestTextProperty);
			set => SetValue(TestTextProperty, value);
		}

		int _appliedCount;
		public int AppliedCount
		{
			get => _appliedCount;
			set
			{
				_appliedCount = value;
				OnPropertyChanged(nameof(AppliedCount));
			}
		}

		int _attachableAppliedCount;
		public int AttachableAppliedCount
		{
			get => _attachableAppliedCount;
			set
			{
				_attachableAppliedCount = value;
				OnPropertyChanged(nameof(AttachableAppliedCount));
			}
		}

		int _readonlyAppliedCount;
		public int ReadonlyAppliedCount
		{
			get => _readonlyAppliedCount;
			set
			{
				_readonlyAppliedCount = value;
				OnPropertyChanged(nameof(ReadonlyAppliedCount));
			}
		}

		int _readonlyAttachableAppliedCount;
		public int ReadonlyAttachableAppliedCount
		{
			get => _readonlyAttachableAppliedCount;
			set
			{
				_readonlyAttachableAppliedCount = value;
				OnPropertyChanged(nameof(ReadonlyAttachableAppliedCount));
			}
		}

		public RaiseOnEqualPage()
		{
			InitializeComponent();
		}

		void Apply_OnClicked(object sender, EventArgs e)
		{
			TestText = TestEntry.Text;
			TestTextReadonly = TestEntry.Text;
			AttachablePropertiesTests.SetTestTextAttachableReadonlyProperty(ReadonlyLabel,TestEntry.Text);
		}
	}

	public static class AttachablePropertiesTests 
	{
		public static readonly BindableProperty TestTextAttachableProperty = BindableProperty.Create("TestTextAttachable",
			true,
			typeof(string),
			typeof(AttachablePropertiesTests),
			propertyChanged:OnTestTextAttachedChanged
		);

		public static readonly BindablePropertyKey TestTextAttachableReadonlyPropertyKey =
			BindableProperty.CreateAttachedReadOnly("TestTextAttachableReadonly",
				true,
				typeof(string),
				typeof(AttachablePropertiesTests),
				default,
				propertyChanged: AttachablePropertiesTestsChanged);

		public static BindableProperty TestTextAttachableReadonlyProperty =>
			TestTextAttachableReadonlyPropertyKey.BindableProperty;

		public static string GetTestTextAttachableReadonlyProperty(BindableObject bindableObject)
		{
			return (string)bindableObject.GetValue(TestTextAttachableReadonlyProperty);
		}

		internal static void SetTestTextAttachableReadonlyProperty(BindableObject bindableObject, string value)
		{
			bindableObject.SetValue(TestTextAttachableReadonlyPropertyKey,value);
		}

		static void AttachablePropertiesTestsChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var label = bindable as Label;
			var page = label?.Parent?.Parent as RaiseOnEqualPage;
			if (page != null)
			{
				page.ReadonlyAttachableAppliedCount += 1;
			}
		}

		static void OnTestTextAttachedChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var label = bindable as Label;
			var page = label?.Parent?.Parent as RaiseOnEqualPage;
			if (page != null)
			{
				page.AttachableAppliedCount += 1;
			}
		}

		public static string GetTestTextAttachable(BindableObject bindableObject)
		{
			return (string)bindableObject.GetValue(TestTextAttachableProperty);
		}

		public static void SetTestTextAttachable(BindableObject bindableObject, string value)
		{
			bindableObject.SetValue(TestTextAttachableProperty,value);
		}
	}
}