using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/coupon")]
[ApiController]
public class CouponApiController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private ResponseDto _response;
    
    public CouponApiController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        _response = new ResponseDto();
    }

    [HttpGet]
    public ResponseDto Get()
    {
        try
        {
            IEnumerable<Coupon> coupons = _db.Coupons.ToList();
            _response.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    
    [HttpGet]
    [Route("{id:int}")]
    public ResponseDto Get(int id)
    {
        try
        {
            var coupon = _db.Coupons.First(x => x.Id == id);
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    [HttpGet]
    [Route("GetByCode/{code}")]
    public ResponseDto GetByCode(string code)
    {
        try
        {
            var coupon = _db.Coupons.First(u => u.Code.ToLower() == code.ToLower());
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    
    [HttpPost]
    public ResponseDto Post([FromBody] CouponDto couponDto)
    {
        try
        {
            var coupon = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Add(coupon);
            _db.SaveChanges();
            
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }
    
        return _response;
    }
    
    
    [HttpPut]
    public ResponseDto Put([FromBody] CouponDto couponDto)
    {
        try
        {
            var coupon = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Update(coupon);
            _db.SaveChanges();
            
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }
    
        return _response;
    }
    
    
    [HttpDelete]
    [Route("{id:int}")]
    public ResponseDto Delete(int id)
    {
        try
        {
            var coupon = _db.Coupons.First(u => u.Id == id);
            _db.Coupons.Remove(coupon);
            _db.SaveChanges();
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }
    
        return _response;
    }
}