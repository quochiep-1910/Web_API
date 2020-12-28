Tầng Service này xử lý các logic

vì mỗi repository thao tác trên 1 bảng vì vậy nếu chúng ta thao tác trên nhiều bảng thì chúng ta nên
tạo ra 1 tầng service để có thể có những phương thức chung gọi tới nhiều repository

điều này làm cho việc độc lập các module, tập trung các nghiệp vụ vào tầng service, Giúp
cho việc API và controller chỉ gọi tập trung vào service thôi, không cần phải đi qua nhiều repository

VD: API muốn gọi bảng [post] [postCategory] [productCategory] thì phải gọi tới 3 bảng repository
vì thế ta có tầng service để chỉ gọi 1 lần duy nhất 