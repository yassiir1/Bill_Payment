using bill_payment.BillDbContext;
using bill_payment.Domains;
using bill_payment.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace bill_payment.MobileAppServices.Settings
{
    public class SettingsServices : ISettingsServices
    {
        private readonly Bill_PaymentContext _billContext;

        public SettingsServices(Bill_PaymentContext billContext)
        {
            _billContext = billContext;
        }

        public async Task<AddBannerResponse> AddBanner(AddBanner data)
        {
            var settings = await _billContext.Setting.FirstOrDefaultAsync();
            if (data.image == null || data.image.Length == 0)
            {
                return new AddBannerResponse
                {
                    Message = "Invalid image file"
                };
            }
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Banners");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(data.image.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await data.image.CopyToAsync(stream);
            }
            var relativePath = $"/Uploads/Banners/{uniqueFileName}";
            var baseUrl = "http://34.60.128.179:8082";

            var input = new Banners()
            {
                image = relativePath,
                path = data.path,
                SettingId = settings.Id
            };
            await _billContext.Banners.AddAsync(input);
            await _billContext.SaveChangesAsync();
            var fullImageUrl = $"{baseUrl}{relativePath}";

            return new AddBannerResponse()
            {
                Message = "Banners Added Successfully",
                data = new BannersOutPut()
                {
                    Id = input.Id,
                    image = fullImageUrl,
                    path = input.path,
                }
            };
        }

        public async Task<AddBannerResponse> EditBanner(int id, AddBanner data)
        {
            var banner = await _billContext.Banners.FindAsync(id);

            if (banner == null)
            {
                return new AddBannerResponse
                {
                    Message = "Banner not found"
                };
            }

            if (data.image != null && data.image.Length > 0)
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", banner.image.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Banners");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(data.image.FileName);
                var newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await data.image.CopyToAsync(stream);
                }

                banner.image = $"/Uploads/Banners/{uniqueFileName}";
            }

            banner.path = data.path;

            await _billContext.SaveChangesAsync();

            var baseUrl = "http://34.60.128.179:8082";
            var fullImageUrl = $"{baseUrl}{banner.image}";

            return new AddBannerResponse
            {
                Message = "Banner updated successfully",
                data = new BannersOutPut
                {
                    Id = banner.Id,
                    image = fullImageUrl,
                    path = banner.path
                }
            };
        }

        public async Task<DeleteBannerResponse> DeleteBanner(int id)
        {
            var Banner = await _billContext.Banners.FindAsync(id);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Banner.image.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _billContext.Banners.Remove(Banner);
            await _billContext.SaveChangesAsync();
            return new DeleteBannerResponse
            {
                Message = "Banner deleted successfully"
            };
        }

        public async Task<SettingsResponse> EditSettings(EditSettings input)
        {
            var data = await _billContext.Setting.Include(c => c.banners).FirstOrDefaultAsync();
            data.primary_color = input.primary_color;
            data.service_columns_number = input.service_columns_number;
            await _billContext.SaveChangesAsync();
            return new SettingsResponse()
            {
                data = new SettingsOutPut()
                {
                    Banners = data.banners.Select(c => new BannersOutPut()
                    {
                        Id = c.Id,
                        image = c.image,
                        path = c.path,
                    }).ToList(),
                    primary_color = data.primary_color,
                    service_columns_number = data.service_columns_number
                },
                Message = "Settings Updated Successfully"
            };
        }

        public async Task<BannersResponse> ListBanners()
        {
            var data = await _billContext.Banners.ToListAsync();
            var baseUrl = "http://34.60.128.179:8082";

            return new BannersResponse()
            {
                Message = "Banners Loaded Successfully",
                data = data.Select(c => new BannersOutPut()
                {
                    Id = c.Id,
                    image = $"{baseUrl}{c.image}",
                    path = c.path,
                }).ToList(),
            };
        }

        public async Task<SettingsResponse> ListSettings()
        {
            var data = await _billContext.Setting.Include(c => c.banners).FirstOrDefaultAsync();
            var baseUrl = "http://34.60.128.179:8082";

            return new SettingsResponse()
            {
                data = new SettingsOutPut()
                {
                    Banners = data.banners.Select(c => new BannersOutPut()
                    {
                        Id = c.Id,
                        image = $"{baseUrl}{c.image}",
                        path = c.path,
                    }).ToList(),
                    primary_color = data.primary_color,
                    service_columns_number = data.service_columns_number
                },
                Message = "Settings Loaded Successfully"
            };
        }
    }
}
