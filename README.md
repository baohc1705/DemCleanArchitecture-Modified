# Báo Cáo Task: Chỉnh sửa Domain và Infrastructure

**Mục tiêu:** 
- Thực hiện tạo Base Repository để giải quyết bài toán CRUD chung cho các thực thể.
- Không dùng liên kết relationship ở database thì làm thế nào khi 2 thực thể có quan hệ 1-n hoặc n-n có thể lấy thông tin show ra được ở method GET
- Kiểm tra source chỉnh chu hơn về format code.
- Chỉnh sửa lại tầng domain và infrastructure rõ ràng.
---

## Phần 1: Tổng hợp kết quả

| # | Task | Chi tiết  | Trạng thái |
|---|----------------|----------------------|------------|
| 1 | **Base Repository** | Dùng mô hình `Generic Repository` | Đã xong |
| 2 | **GET dữ liệu 2 thực thể n-n** | - | Chưa hoàn thành |
| 3 | **Format Code** | Sử dụng `Camel Case` và `Pascal Case` | Đã xong |

---

## Phần 2: Chi tiết 

### Task 1: Base Repository

Tạo một interface Base Repository với entity là trừu tượng để các phương thức khác có thể kế thừa
```csharp

public interface IBaseRepository<T, TKey> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(TKey key);
    Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    // Test IQueryable
    Task<IQueryable<T>> GetAllQueryAsync();
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}

```
Các repository lần lượt kế thừa. Ví dụ NewsRepository

```csharp
public interface INewsRepository : IBaseRepository<News, int> { }

public class NewsRepository : BaseRepository<News, int>, INewsRepository
{
    public NewsRepository(AppDbContext context) : base(context) { }
}
```
###  Task 2: GET Data khi entity có quan hệ n-n

#### 1. Cơ sở dữ liệu (Không có liên kết giữa các bảng)

Cơ sở dữ liệu:

![Danh sách liên hệ](https://github.com/baohc1705/DemCleanArchitecture-Modified/blob/main/screenshot/db.png)

Domain entity:

- Base Entity: Lớp trừu tượng chứa thông tin chung

```csharp

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public int? CreatedBy { get; set; } = 1;
    public int? UpdatedBy { get; set; } = 1;
}

```
- Menu Entity

```csharp
public class Menu : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public ICollection<NewsMenu> NewsMenu { get; set; } = new List<NewsMenu>();
}

```
- News entity

```csharp
public enum NewsStatus
{
    Draft = 0,
    Scheduled = 1,
    Published = 2,
    Archived = 3
}
public class News : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Summary { get; set; }
    public string? Content { get; set; }
    public string? ThumbnailUrl { get; set; }
    public NewsStatus Status { get; set; }
    public DateTime? PublishedAt { get; set; }
    public int ViewCount { get; set; }
    public bool IsActive { get; set; }
    public ICollection<NewsMenu> NewsMenu { get; set; } = new List<NewsMenu>();
}
```
- NewsMenu: Bảng trung gian quản lý quan hệ n-n

```csharp
public class NewsMenu : BaseEntity
{
    public int NewsId { get; set; }
    public int MenuId { get; set; }
    public bool IsActive { get; set; }
    public  News News { get; set; }
    public  Menu Menu { get; set; }
}

```


#### 2. Thực hiện map dữ liệu

Sử dụng `IEntityTypeConfiguration` để map các cột dữ liệu nếu tên các cột dữ liệu trong database và trong entity (code) khác nhau

#### 3. Get dữ liệu của 2 thực thể có quan hệ n-n

Hướng làm hiện tại:

- Hướng 1: Gọi từng dữ liệu của Menu và News từ Repository lên Application để map lại qua DTO (tốc độ chậm vì query 3 lần)
- Hướng 2: Gọi dữ liệu bằng `IQueryable` lên Application rồi dùng `LinQ` join lại (Application có EF Core không sạch)
- Hướng 3: Xử lý dữ liệu bằng `Dapper` join lại với nhau ở dưới Infrastructure (tốc độ đọc nhanh do RawSQL)

#### 4. Format lại code

- Format lại tên biến sử dụng Camel Case
- Format lại tên hàm, class sử dụng Pascal Case
- Xóa bỏ các biến, phương thức và thư viện không dùng

#### 5. Chỉnh sửa lại tầng Domain

- Tầng domain rõ ràng không còn lẫn logic
- Tầng infrastructure sử dụng mô hình `Generic Repository` tránh việc viết code lại các CRUD

## Phần 3: Tổng kết

- Task 1, 3 cơ bản đã hoàn thành.
- Task 2 chưa hoàn thành theo đúng yêu cầu, chưa tìm ra giải pháp.
