using LoginFormWPF.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoginFormWPF.Repositories
{
	public class UserRepository : RepositoryBase, IUserRepository
	{
		public void Add(UserModel userModel)
		{
			throw new NotImplementedException();
		}

		public bool AuthenticateUser(NetworkCredential crredential)
		{
			bool validUser;
			using (var connection = GetConnection())		// 開啟資料庫連線
			{
				using (var command = new SqlCommand())		// 創建SQL指令
				{
					connection.Open();						// 開啟連線
					command.Connection = connection;        // 設置指令Command使用的資料庫連線

					#region 這是一段防止SQL Injection的方法，透過將輸入參數化查詢，防止惡意代碼
					command.CommandText = "SELECT * FROM [UserTable] WHERE UserName=@UserName and [Password]=@Password";         // 定義SQL查詢
					command.Parameters.Clear();
					command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = crredential.UserName;		// 將使用者名稱作為參數加入查詢
					command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = crredential.Password;       // 將密碼作為參數加入查詢
					#endregion

					validUser = command.ExecuteScalar() == null ? false : true;		// 執行查詢並檢查結果，ExecuteScalar執行查詢並返回第一行第一列數據的方法
				}
			}
			return validUser;
		}

		public void Edit(UserModel userModel)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<UserModel> GetByAll()
		{
			throw new NotImplementedException();
		}

		public UserModel GetById(int id)
		{
			throw new NotImplementedException();
		}

		public UserModel GetByUserName(string userName)
		{
			UserModel userModel = null;
			using (var connection = GetConnection())        // 開啟資料庫連線
			{
				using (var command = new SqlCommand())      // 創建SQL指令
				{
					connection.Open();                      // 開啟連線
					command.Connection = connection;        // 設置指令Command使用的資料庫連線

					#region 這是一段防止SQL Injection的方法，透過將輸入參數化查詢，防止惡意代碼
					command.CommandText = "SELECT * FROM [UserTable] WHERE UserName=@UserName";         // 定義SQL查詢
					command.Parameters.Clear();
					command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = userName;       // 將使用者名稱作為參數加入查詢
					#endregion

					using (var reader = command.ExecuteReader())
					{
						if(reader.Read())
						{
							userModel = new UserModel()
							{
								Id = reader[0].ToString(),
								UserName = reader[1].ToString(),
								Password = string.Empty,
								Name = reader[2].ToString(),
								Email = reader[3].ToString(),
							};
						}
					}
				}
			}
			return userModel;
		}

		public void Remove(int id)
		{
			throw new NotImplementedException();
		}
	}
}
