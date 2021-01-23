using System.Collections.Generic;
using System.Linq;
using WebAPI.Common;
using WebAPI.Data.Infrastructure;
using WebAPI.Data.Repositories;
using WebAPI.Model.Models;

namespace WebAPI.Service
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(Product Product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);

        IEnumerable<Product> GetLastest(int top);

        IEnumerable<Product> GetHotProduct(int top);

        Product GetById(int id);

        void Save();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        private IUnitOfWork _unitOfWork;

        private ITagRepository _tagRepository;

        private IProductTagRepository _productTagRepository;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork,
           IProductTagRepository productTagRepository, ITagRepository tagRepository)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;

            this._productTagRepository = productTagRepository;
            this._tagRepository = tagRepository;
        }

        public Product Add(Product Product)
        {
            var product = _productRepository.Add(Product);
            _unitOfWork.Commit(); //commit lần 1 để có giá trị(Id, name,..) để truyền xuống if
            if (!string.IsNullOrEmpty(Product.Tags))//nếu product.tag null or empty
            {
                string[] tags = Product.Tags.Split(','); //tách chuỗi nhập vào bằng dấu [,]
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]); //chuyển đổi tag theo StringHelper
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)//nếu = 0thì tạo mới tag
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
            return product;
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        /// <summary>
        /// Get all for search
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))//so sánh khác rỗng (nên dùng cái này thay cho [keyword !=null]
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetLastest(int top)
        {
            return _productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product Product)
        {
            _productRepository.Update(Product);
            if (!string.IsNullOrEmpty(Product.Tags))//nếu product.tag null or empty
            {
                string[] tags = Product.Tags.Split(','); //tách chuỗi nhập vào khi gặp dấu [,]
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]); //chuyển đổi tag theo StringHelper
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)//nếu = 0thì tạo mới tag
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == Product.ID);//muốn update thì phải xoá hết bản ghi cũ đi
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
        }
    }
}