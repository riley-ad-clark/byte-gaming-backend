using DAL;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IProductService
    {
        Task<bool> AddProductService(ProductDTO productDTO);
        Task<List<Product>> GetAllProductsService();
        Task<Product> GetProductBySlugService(string slug);
        Task<bool> DeleteProductService(int id);
        Task<bool> EditProductService(Product product);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;
        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> AddProductService(ProductDTO productDTO)
        {
            return await repository.AddProductRepo(productDTO);
        }

        public async Task<List<Product>> GetAllProductsService()
        {
            return await repository.GetAllProductsRepo();
        }

        public async Task<Product> GetProductBySlugService(string slug)
        {
            return await repository.GetProductBySlugRepo(slug);
        }

        public async Task<bool> DeleteProductService(int id)
        {
            return await repository.DeleteProductRepo(id);
        }

        public async Task<bool> EditProductService(Product product)
        {
            return await repository.EditProductRepo(product);
        }
    }
}
