using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoginFormWPF.Model
{
	/// <summary>
	/// 用來定義與使用者資料庫進行交互的Contract
	/// </summary>
	public interface IUserRepository
	{
		// NetworkCredential是一個封裝了網路憑證(如使用者名稱密碼)的類別
		// 包含UserName, Password, Domain，用來進行驗證時提供使用者的身分憑證
		bool AuthenticateUser(NetworkCredential crredential);
		void Add(UserModel userModel);
		void Edit(UserModel userModel);
		void Remove(int id);
		UserModel GetById(int id);
		UserModel GetByUserName(string userName);
		IEnumerable<UserModel> GetByAll();
	}
}
