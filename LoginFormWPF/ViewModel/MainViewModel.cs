using LoginFormWPF.Model;
using LoginFormWPF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LoginFormWPF.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private UserAccoutModel _currentUserAccout;
		private IUserRepository _userRepository;

		public UserAccoutModel CurrentUserAccout
		{
			get { return _currentUserAccout; }
			set
			{
				_currentUserAccout = value;
				OnPorpertyChanged(nameof(CurrentUserAccout));
			}
		}

		public MainViewModel()
		{
			_userRepository = new UserRepository();
			CurrentUserAccout = new UserAccoutModel();
			LoadCurrentUserData();
		}

		private void LoadCurrentUserData()
		{
			var user = _userRepository.GetByUserName(Thread.CurrentPrincipal.Identity.Name);
			if (user != null)
			{
				CurrentUserAccout.UserName = user.UserName;
				CurrentUserAccout.DisplayName = $"Welcome {user.UserName}!";
				CurrentUserAccout.ProfilePicutre = null;
			}
			else
			{
				CurrentUserAccout.DisplayName = "Invalid user, not logged in.";

				// Hide chile views
			}
		}
	}
}
