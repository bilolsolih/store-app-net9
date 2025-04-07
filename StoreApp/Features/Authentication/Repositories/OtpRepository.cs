using Microsoft.EntityFrameworkCore;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Authentication.Repositories;

public class OtpRepository(StoreDbContext context)
{
  public async Task<Otp> AddOtpAsync(Otp otp)
  {
    context.Otps.Add(otp);
    await context.SaveChangesAsync();
    return otp;
  }

  public async Task<Otp?> GetByIdAsync(int id)
  {
    return await context.Otps.FindAsync(id);
  }

  public async Task<Otp?> GetByEmailAndCodeAsync(string email, string code)
  {
    return await context.Otps.Include(o => o.User)
      .SingleOrDefaultAsync(o => o.User.Email.ToLower() == email.ToLower() && o.Code == code);
  }

  public async Task<bool> ExistsByEmailAndCodeAsync(string email, string code)
  {
    return await context.Otps.Include(o => o.User).AnyAsync(o => o.User.Email == email && o.Code == code);
  }

  public async Task<IEnumerable<Otp>> GetAllByUserEmailAsync(string email)
  {
    return await context.Otps.Include(o => o.User).Where(o => o.User.Email == email).ToListAsync();
  }

  public async Task DeleteAllAsync(IEnumerable<Otp> otps)
  {
    context.Otps.RemoveRange(otps);
    await context.SaveChangesAsync();
  }
}