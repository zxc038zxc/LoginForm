using LoginFormWPF.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LoginFormWPF
{
	/// <summary>
	/// App.xaml 的互動邏輯
	/// </summary>
	public partial class App : Application
	{
		protected void ApplicationStart(object sender, StartupEventArgs e)
		{
			var loginView = new LoginView();
			loginView.Show();
			loginView.IsVisibleChanged += (s, ev) =>
			  {
				  if (loginView.IsVisible == false && loginView.IsLoaded)
				  {
					  var mainView = new MainView();
					  mainView.Show();
					  loginView.Close();
				  }
			  };
		}
	}
}
