using Frontend.Interfaces;
using System.Net.Http.Headers;
using Application.Dtos.Product;
using Domain.Entities;

namespace Frontend.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            SetRequestHeaders();
        }

        private void SetRequestHeaders()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFkbWluQG1haWwuY29tIiwiZ2l2ZW5fbmFtZSI6ImFkbWluIiwic3ViIjoiNTFmNmJlNjItOTZhMi00NzYwLTgyNTQtYWQyYWUzM2ZhOGYwIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNzUzOTg2Mjg3LCJleHAiOjE3NTQ1OTEwODcsImlhdCI6MTc1Mzk4NjI4NywiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIzMSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMzEifQ.notwvIKHoQaD4ULY-01eP0HPMg2DwDi34qHw9E3yG8c");
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            List<Product>? products = await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
            return products ?? [];
        }

        public async Task<Product> AddProductAsync(UpdateProductDto product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", product);

            if (response.IsSuccessStatusCode)
            {
                var updatedProduct = await response.Content.ReadFromJsonAsync<Product>();
                return updatedProduct ?? throw new InvalidOperationException("Product creation returned null.");
            }

            throw new HttpRequestException($"Error adding product: {response.ReasonPhrase}");
        }

        public async Task EditProductAsync(UpdateProductDto product, int id)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", product);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error updating product: {response.ReasonPhrase}");
            }
        }

        public async Task<UpdateProductDto> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<UpdateProductDto>();
                return product ?? throw new InvalidOperationException("Product not found.");
            }

            throw new HttpRequestException($"Error retrieving product: {response.ReasonPhrase}");
        }

        public async Task DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error deleting product: {response.ReasonPhrase}");
            }
        }
    }
}
