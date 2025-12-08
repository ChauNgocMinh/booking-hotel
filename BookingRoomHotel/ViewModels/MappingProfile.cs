using AutoMapper;
using BookingRoomHotel.Models;
using BookingRoomHotel.ViewModels;

namespace BookingRoomHotel.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<TaoLoaiPhongViewModel, LoaiPhong>();
            CreateMap<DatPhongViewModel, DatPhong>();
            CreateMap<TaoLoaiPhongViewModel, Phong>();
        }
    }
}
