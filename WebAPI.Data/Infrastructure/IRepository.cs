using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebAPI.Data.Infrastructure
{
    //Interface này tạo ra các phương thức dùng chung cho việc truy xuất dữ liệu
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        T Add(T entity);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed
        T Delete(T entity);

        T Delete(int id);

        //Delete multi records
        void DeleteMulti(Expression<Func<T, bool>> where);

        // lấy ra 1 đối tượng theo id
        T GetSingleById(int id);

        // lấy ra 1 đối tượng theo 1 điều kiện
        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        //lấy ra tất cả
        IEnumerable<T> GetAll(string[] includes = null);

        //lấy ra nhiều
        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        //lấy ra nhiều đối tượng và sắp xếp theo từng trang
        IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        //đếm xem có bao nhiêu đối tượng
        int Count(Expression<Func<T, bool>> where);

        //kiểm tra xem đối tượng có chứa 1 đối tượng nào đó không
        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}