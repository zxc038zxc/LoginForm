using LoginFormWPF.Model;
using LoginFormWPF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoginFormWPF.ViewModel
{
	public class LoginViewModel : ViewModelBase
	{
		private string _userName;
		// .net core, .net5 之後，已不建議使用
		// 存儲敏感信息，確保在使用後清除內存。但他並不能完全防止敏感數據在內存忠的洩漏，若要更安全就必須使用加密字符串等其他方式
		private SecureString _password;
		private string _errorMsg;
		private bool _isViewVisible = true;

		private IUserRepository _userRepository;

		#region Property
		public string UserName
		{
			get
			{
				return _userName;
			}
			set
			{
				_userName = value;
				OnPorpertyChanged(nameof(UserName));
			}
		}
		public SecureString Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
				OnPorpertyChanged(nameof(Password));
			}
		}
		public string ErrorMsg
		{
			get
			{
				return _errorMsg;
			}
			set
			{
				_errorMsg = value;
				OnPorpertyChanged(nameof(ErrorMsg));
			}
		}
		public bool IsViewVisible
		{
			get
			{
				return _isViewVisible;
			}
			set
			{
				_isViewVisible = value;
				OnPorpertyChanged(nameof(IsViewVisible));
			}
		}
		#endregion

		#region Commands
		// 公開訪問，隱藏設定
		public ICommand LoginCommand { get; }
		public ICommand RecoverPwdCommand { get; }
		public ICommand ShowPwdCommand { get; }
		public ICommand RememberPwdCommand { get; }
		#endregion

		public LoginViewModel()
		{
			LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
			RecoverPwdCommand = new ViewModelCommand(x => ExecuteRecoverPwdCommand("", ""));
			_userRepository = new UserRepository();
		}

		private bool CanExecuteLoginCommand(object obj)
		{
			bool validData = true;
			if (string.IsNullOrEmpty(UserName) || UserName.Length < 3 || Password == null || Password.Length < 3)
			{
				validData = false;
			}
			return validData;
		}

		private void ExecuteLoginCommand(object obj)
		{
			// 封裝登入憑證
			var isValidUser = _userRepository.AuthenticateUser(new System.Net.NetworkCredential(UserName, Password));
			if(isValidUser)
			{
				// 這段將當前線程的Principal設置為一個新的GenericPrincipal物件
				// GenericPrincipal是用來表示一個通用的使用者對象，它可以用來儲存身分和腳色
				Thread.CurrentPrincipal = new GenericPrincipal(
					new GenericIdentity(UserName),null);
				IsViewVisible = false;
			}
			else
			{
				ErrorMsg = "* Invalid userName or password";
			}
		}

		private void ExecuteRecoverPwdCommand(string userName, string email)
		{
			throw new NotImplementedException();
		}
	}
}