ở Service Test này
ở tầng này chúng ta gọi tầng Serice để xử lý các câu lệnh

chúng ta dùng tới Moq tức là tạo 1 đối tượng giả để chạy thử cái chức năng mà 
mình viết ra (chức năng này có thể chưa hoàn thiện).

VD: ta tạo 1 dữ liệu ảo của [PostCategory] có 3 giá trị ID
thì ta 'Assert' nó xem dữ liệu đầu vào có đúng ko, hay có lỗi gì khi add không!