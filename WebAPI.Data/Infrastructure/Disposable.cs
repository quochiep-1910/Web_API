using System;

namespace WebAPI.Data.Infrastructure
{
    //đây là phương pháp chủ yếu dùng giải phóng các tài nguyên không được quản lý
    public class Disposable : IDisposable
    {
        private bool isDisposed;

        /// <summary>
        /// Huỷ thu hồi bộ nhớ
        /// </summary>
        ~Disposable()
        {
            Dispose(false);
        }

        /// <summary>
        /// Thu hồi bộ nhớ
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }

            isDisposed = true;
        }

        /// <summary>
        /// Ghi đè điều này để loại bỏ các đối tượng tùy chỉnh
        /// </summary>
        protected virtual void DisposeCore()
        {
        }
    }
}