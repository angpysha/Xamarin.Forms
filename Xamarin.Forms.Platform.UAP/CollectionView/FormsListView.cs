﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWPApp = Windows.UI.Xaml.Application;
using UWPControlTemplate = Windows.UI.Xaml.Controls.ControlTemplate;

namespace Xamarin.Forms.Platform.UWP
{
	internal class FormsListView : Windows.UI.Xaml.Controls.ListView, IEmptyView
	{
		ContentControl _emptyViewContentControl;
		FrameworkElement _emptyView;
		View _formsEmptyView;

		public FormsListView()
		{
			Template = (UWPControlTemplate)UWPApp.Current.Resources["FormsListViewTemplate"];
		}

		public static readonly DependencyProperty EmptyViewVisibilityProperty =
			DependencyProperty.Register(nameof(EmptyViewVisibility), typeof(Visibility),
				typeof(FormsListView), new PropertyMetadata(Visibility.Collapsed));

		public Visibility EmptyViewVisibility
		{
			get { return (Visibility)GetValue(EmptyViewVisibilityProperty); }
			set { SetValue(EmptyViewVisibilityProperty, value); }
		}

		public void SetEmptyView(FrameworkElement emptyView, View formsEmptyView)
		{
			_emptyView = emptyView;
			_formsEmptyView = formsEmptyView;

			if (_emptyViewContentControl != null)
			{
				_emptyViewContentControl.Content = emptyView;
			}
		}

		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_emptyViewContentControl = GetTemplateChild("EmptyViewContentControl") as ContentControl;

			if (_emptyView != null)
			{
				_emptyViewContentControl.Content = _emptyView;
			}
		}

		protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
		{
			if (_formsEmptyView != null)
			{
				_formsEmptyView.Layout(new Rectangle(0, 0, finalSize.Width, finalSize.Height));
			}

			return base.ArrangeOverride(finalSize);
		}

		protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
			GroupFooterItemTemplateContext.EnsureSelectionDisabled(element, item);
			base.PrepareContainerForItemOverride(element, item);
		}
	}
}