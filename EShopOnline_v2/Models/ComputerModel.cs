using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Models
{
    public class ComputerModel
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string HangSx { get; set; }
        public string Loai { get; set; }
        public CPU Cpu { get; set; }
        public RAM Ram { get; set; }
        public OCung Ocung { get; set; }
        public CardVGA CardVga { get; set; }
        public  string Hdh { get; set; }
        public string Chipset { get; set; }
        public string Oquang { get; set; }
        public string CongGiaoTiep { get; set; }
        public string Lan { get; set; }
        public int SoLuong { get; set; }
        public string NamSx { get; set; }
        public int BaoHanh { get; set; }
        public int Gia { get; set; }
        public int GiamGia { get; set; }

        public string LinkImg1 { get; set; }
        public string LinkImg2 { get; set; }
        public string LinkImg3 { get; set; }

        public ComputerModel(int id, string ten, string hangSx, string loai, CPU cpu, RAM ram, OCung oCung, 
            CardVGA cardVga, string hdh, string chipset, string oQuang, string congGiaoTiep, string lan, int soLuong, 
            string namSx, int baoHanh, int gia, int giamGia, string linkImg1, string linkImg2, string linkImg3)
        {
            Id = id;
            Ten = ten;
            HangSx = hangSx;
            Loai = loai;
            Cpu = cpu;
            Ram = ram;
            CardVga = cardVga;
            Ocung = oCung;
            Hdh = hdh;
            Chipset = chipset;
            Oquang = oQuang;
            CongGiaoTiep = congGiaoTiep;
            Lan = lan;
            SoLuong = soLuong;
            NamSx = namSx;
            BaoHanh = baoHanh;
            Gia = gia;
            GiamGia = giamGia;
            LinkImg1 = linkImg1;
            LinkImg2 = linkImg2;
            LinkImg3 = linkImg3;
        }
    }
}