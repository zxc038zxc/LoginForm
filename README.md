# LoginForm
WPF + SQL Server + MVVM

# 登入介面
![image](https://github.com/user-attachments/assets/a14ec672-67f5-408e-86a9-ef9cc397467a)

# MVVM架構
![image](https://github.com/user-attachments/assets/449549ca-f42f-475c-ad3e-7ef89e2c4af0)

## 1. View -> ViewModel
透過Binding DataContext，將View的元素與ViewModel綁定
讓View可以顯示ViewModel的資料

### 密碼型態使用SecureString，避免以明文形式存在於數據中
### 由於SecrueString無法直接Binding，於是撰寫一個CustomControl，透過DependencyProperty依賴屬性進行綁定

![image](https://github.com/user-attachments/assets/1327811a-43c1-4b6a-9967-aed5432c61ff)

## 2. ViewModel -> Model
由於設計的Loging視窗，只需要抓取資料庫內的帳號密碼，所以這邊Model為資料庫存取

### 1. 撰寫抽象 ```RepositoryBase```，每個Repository繼承此

![image](https://github.com/user-attachments/assets/b9238ea9-b896-484d-b2ce-c60a2a45583b)
![image](https://github.com/user-attachments/assets/e59fa0d4-5ad3-4856-ae79-06c03e0a2e26)

#### 資料庫查詢避免 ```SQLInjection攻擊```：參數化查詢
透過參數化查詢的方式，定義輸入為獨立的參數處理，避免惡意攻擊，保證應用程序與資料庫進行交互時更加安全
![image](https://github.com/user-attachments/assets/d6eeb1ae-2d3b-4a74-bd3f-d7087d49c47f)

### 2. 定義User資料庫介面，讓ViewModel內 **依賴抽象**，ViewModel不需要依賴真實的資料來源，使得程式彈性上升、耦合降低
![image](https://github.com/user-attachments/assets/31bc5ea8-89b3-4de9-a7b9-6371ba6fb874)

## 3. ViewModel -> View

### 1. 撰寫介面 ViewModelBase，繼承 ```INotifyPropertyChanged```
繼承此介面的ViewModel，呼叫底層的PropertyChanged來通知View進行參數的更新
![image](https://github.com/user-attachments/assets/5628be40-395a-4d07-a757-ab897b1726d3)
![image](https://github.com/user-attachments/assets/acaa00d6-04e5-4f9f-8f71-66e6ba1a746f)

### 2. 撰寫```ViewModelCommand```，用來串接View要使用的Command，將UI事件(按鈕點擊)與業務邏輯(用戶登入)分離。使程式更加模塊化利於測試
![image](https://github.com/user-attachments/assets/437ecc55-ec62-4cca-a7af-e3b2964e4347)

#### ViewModel宣告```ICommand```，在建構式中定義執行事件，將責任分離
![image](https://github.com/user-attachments/assets/00777bc3-1637-4d0f-857e-91723bdb9810)

