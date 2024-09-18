using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginFormWPF.CustomControl
{
	/// <summary>
	/// BindablePwdBoxControl.xaml 的互動邏輯
	/// </summary>
	public partial class BindablePwdBoxControl : UserControl
	{
		public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(
			"Password", typeof(SecureString), typeof(BindablePwdBoxControl));

		public SecureString Password
		{
			get { return (SecureString)GetValue(PasswordProperty); }
			set { SetValue(PasswordProperty, value); }
		}

		public BindablePwdBoxControl()
		{
			InitializeComponent();

			TxtPwdBox.PasswordChanged += OnPwdChanged;
		}

		private void OnPwdChanged(object sender, RoutedEventArgs e)
		{
			Password = TxtPwdBox.SecurePassword;
		}
	}
}
