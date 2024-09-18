using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoginFormWPF.ViewModel
{
	/// <summary>
	/// 在ViewModel中定義可以綁定到View的命令。
	/// 可以將按鈕點擊等動作與具體的處理邏輯分離
	/// </summary>
	public class ViewModelCommand : ICommand
	{
		// Fields
		private readonly Action<object> _executeAction;
		private readonly Predicate<object> _canExecuteAction;

		public ViewModelCommand(Action<object> executeAction)
		{
			_executeAction = executeAction;
			_canExecuteAction = null;
		}
		public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
		{
			_executeAction = executeAction;
			_canExecuteAction = canExecuteAction;
		}

		/// <summary>
		/// 在命令是否可以執行的狀態發生改變時觸發，讓WPF的UI知道命令的可執行狀態是否改變
		/// CommandManager.RequerySuggested是WPF自動處理命令啟用/禁用的機制
		/// </summary>
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value;}
		}

		public bool CanExecute(object parameter)
		{
			return _canExecuteAction == null ? true: _canExecuteAction(parameter);
		}

		public void Execute(object parameter)
		{
			_executeAction?.Invoke(parameter);
		}
	}
}
