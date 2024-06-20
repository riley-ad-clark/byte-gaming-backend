using Entities.Context;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsRepo();
        Task<bool> AddProductRepo(ProductDTO pdto);
        Task<Product> GetProductBySlugRepo(string slug);
        Task<bool> DeleteProductRepo(int id);
        Task<bool> EditProductRepo(Product product);
    }

    public class ProductRepository : IProductRepository
    {
        ByteGamingContext context = new ByteGamingContext();

        public async Task<List<Product>> GetAllProductsRepo()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<bool> AddProductRepo(ProductDTO pdto)
        {
            if (pdto == null)
            {
                return false;
            }
            else if (pdto.ProductPrice <= 0 || pdto.ProductPrice == null)
            {
                return false;
            }
            else if (pdto.ProductName == null || pdto.ProductName.Length == 0)
            {
                return false;
            }
            else if (pdto.ProductDescription == null || pdto.ProductDescription.Length == 0)
            {
                return false;
            } else if (pdto.ProductImageURL == null || pdto.ProductDescription.Length == 0)
            {
                return false;
            } else if (pdto.ProductSlug == null || pdto.ProductSlug.Length == 0 || pdto.ProductSlug.Contains(' '))
            {
                return false;
            }
            else
            {
                Product product = new Product
                {
                    ProductName = pdto.ProductName,
                    ProductDescription = pdto.ProductDescription,
                    ProductPrice = pdto.ProductPrice,
                    ProductImageURL = pdto.ProductImageURL,
                    ProductSlug = pdto.ProductSlug,
                };

                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<Product> GetProductBySlugRepo(string slug)
        {
            var product = await context.Products.Where(x => x.ProductSlug == slug).FirstOrDefaultAsync();

            if (product != null)
            {
                return product;
            } else
            {
                return null;
            }
        }

        public async Task<bool> DeleteProductRepo(int id)
        {
            var product = await context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();

            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<bool> EditProductRepo(Product product)
        {
            var foundProduct = await context.Products.Where(x => x.ProductSlug == product.ProductSlug).FirstOrDefaultAsync();

            if (foundProduct != null)
            {
                foundProduct.ProductPrice = product.ProductPrice;
                foundProduct.ProductName = product.ProductName;
                foundProduct.ProductDescription = product.ProductDescription;
                foundProduct.ProductImageURL = product.ProductImageURL;
                foundProduct.ProductSlug = product.ProductSlug;

                await context.SaveChangesAsync();
                return true;
            } else {
                return false;
            }
        }
    }
}
