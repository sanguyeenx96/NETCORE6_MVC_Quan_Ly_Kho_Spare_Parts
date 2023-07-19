using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webspare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using webspare.Helpers;
using System.Collections;
using Microsoft.AspNetCore.Authentication;

namespace webspare.Controllers
{
    public class HomeController : Controller
    {
        //Khai báo lớp DbContext:
        private WebSparePartContext db = new WebSparePartContext();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Đăng nhập:
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(TblAdmin ad)
        {
            TblAdmin a = db.TblAdmins.Where(x => x.AdName == ad.AdName && x.AdPass == ad.AdPass).SingleOrDefault();
            if (a != null)
            {
                ISession session = HttpContext.Session;

                session.SetString("user", a.Hoten);
                var hoten = session.GetString("user");
                TempData["hoten"] = hoten;
                TempData.Keep();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.msg = "Sai tên đăng nhập hoặc mật khẩu";
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Danhsachdanghet()
        {
            var list = await db.Danhsachlinhkiens.Where(x => x.Tonkho == 0).ToListAsync();
            return View(list);
        }

        //Trang chủ:
        public async Task<IActionResult> Index(string model)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.tenmodel = new SelectList(db.Models, "Tenmodel", "Tenmodel"); // danh sách
            TempData["sl"] = db.Danhsachlinhkiens.Count();
            TempData["slhet"] = db.Danhsachlinhkiens.Where(x => x.Tonkho == 0).Count();
            TempData["sldangyeucau"] = db.Dathangs.Where(x => x.Trangthai == "Yêu cầu đặt hàng").Count();
            TempData["sldangdat"] = db.Dathangs.Where(x => (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu")).Count();

            TempData["slIJP"] = db.Danhsachlinhkiens.Where(x => x.Model == "IJP").Count();
            TempData["slhet_IJP"] = db.Danhsachlinhkiens.Where(x => (x.Model == "IJP" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_IJP"] = db.Dathangs.Where(x => (x.Model == "IJP" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_IJP"] = db.Dathangs.Where(x => (x.Model == "IJP" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

            TempData["slLD2"] = db.Danhsachlinhkiens.Where(x => x.Model == "LD2").Count();
            TempData["slhet_LD2"] = db.Danhsachlinhkiens.Where(x => (x.Model == "LD2" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_LD2"] = db.Dathangs.Where(x => (x.Model == "LD2" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_LD2"] = db.Dathangs.Where(x => (x.Model == "LD2" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

            TempData["slhontai"] = db.Danhsachlinhkiens.Where(x => x.Model == "hontai").Count();
            TempData["slhet_hontai"] = db.Danhsachlinhkiens.Where(x => (x.Model == "hontai" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_hontai"] = db.Dathangs.Where(x => (x.Model == "hontai" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_hontai"] = db.Dathangs.Where(x => (x.Model == "hontai" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

            TempData["slfix"] = db.Danhsachlinhkiens.Where(x => x.Model == "fixing").Count();
            TempData["slhet_fix"] = db.Danhsachlinhkiens.Where(x => (x.Model == "fixing" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_fix"] = db.Dathangs.Where(x => (x.Model == "fixing" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_fix"] = db.Dathangs.Where(x => (x.Model == "fixing" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

            TempData["slcsd"] = db.Danhsachlinhkiens.Where(x => x.Model == "csd").Count();
            TempData["slhet_csd"] = db.Danhsachlinhkiens.Where(x => (x.Model == "csd" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_csd"] = db.Dathangs.Where(x => (x.Model == "csd" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_csd"] = db.Dathangs.Where(x => (x.Model == "csd" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

            TempData["slitb"] = db.Danhsachlinhkiens.Where(x => x.Model == "itb-driver").Count();
            TempData["slhet_itb"] = db.Danhsachlinhkiens.Where(x => (x.Model == "itb-driver" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_itb"] = db.Dathangs.Where(x => (x.Model == "itb-driver" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_itb"] = db.Dathangs.Where(x => (x.Model == "itb-driver" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();
            if (!String.IsNullOrEmpty(model))
            {
                TempData["select_model"] = model;
                TempData["sl"] = db.Danhsachlinhkiens.Where(x => x.Model == model).Count();
                TempData["slhet"] = db.Danhsachlinhkiens.Where(x => (x.Model == model && x.Tonkho == 0)).Count();
                TempData["sldangyeucau"] = db.Dathangs.Where(x => (x.Model == model && x.Trangthai == "Yêu cầu đặt hàng")).Count();
                TempData["sldangdat"] = db.Dathangs.Where(x => (x.Model == model && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

                ViewData["mdl"] = model;
                var list = await db.Danhsachlinhkiens.Where(x => x.Model == model).ToListAsync();
                var tsl = list.Count();
                ViewData["tsl"] = tsl;

                return View(list);
            }
            return View();
        }

        //Trang thêm linh kiện:
        public IActionResult Themlinhkien()
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.tenmodel = new SelectList(db.Models, "Tenmodel", "Tenmodel");
            return View();
        }

        [HttpPost]
        public IActionResult Themlinhkien_thucong(Danhsachlinhkien linhkien)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            try
            {
                Danhsachlinhkien dslk = new Danhsachlinhkien();
                dslk.Model = linhkien.Model;
                if (!String.IsNullOrEmpty(linhkien.Tenjig))
                {
                    dslk.Tenjig = linhkien.Tenjig.Trim();
                }
                if (!String.IsNullOrEmpty(linhkien.Majig))
                {
                    dslk.Majig = linhkien.Majig.Trim();
                }
                if (!String.IsNullOrEmpty(linhkien.Tenlinhkien))
                {
                    dslk.Tenlinhkien = linhkien.Tenlinhkien.Trim();
                }
                if (!String.IsNullOrEmpty(linhkien.Malinhkien))
                {
                    dslk.Malinhkien = linhkien.Malinhkien.Trim();
                }
                if (!String.IsNullOrEmpty(linhkien.Maker))
                {
                    dslk.Maker = linhkien.Maker.Trim();
                }
                dslk.Donvi = linhkien.Donvi;
                dslk.Dongia = linhkien.Dongia;
                dslk.Tonkho = linhkien.Tonkho;
                dslk.Ghichu = linhkien.Ghichu;
                dslk.Image = linhkien.Image;

                var checktrungsql = db.Danhsachlinhkiens.Where(x => (x.Malinhkien == linhkien.Malinhkien && x.Model == linhkien.Model)).Count();
                if (checktrungsql == 0)
                {
                    db.Danhsachlinhkiens.Add(dslk);
                    db.SaveChanges();
                    TempData["OK"] = "1";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["TRUNG"] = "1";

                    return RedirectToAction("Themlinhkien");
                }
            }
            catch
            {
                TempData["NG"] = "1";
                return RedirectToAction("Index");
            }
        }

        //Chức năng Thêm hình ảnh:
        public async Task<IActionResult> Themhinhanh(int? id)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            Danhsachlinhkien danhsachlinhkien = await db.Danhsachlinhkiens.FindAsync(id);
            if (danhsachlinhkien == null)
            {
                return NotFound();
            }
            return View(danhsachlinhkien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themhinhanh(int id, [Bind("Id,Model,Tenjig,Majig,Tenlinhkien,Malinhkien,Maker,Donvi,Dongia,Tonkho,Ghichu,Image")] Danhsachlinhkien danhsachlinhkien)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            if (id != danhsachlinhkien.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    TempData["OK"] = "1";
                    db.Update(danhsachlinhkien);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            string modelhientai = TempData["select_model"].ToString();
            TempData.Keep();
            return RedirectToRoute(new { action = "Index", controller = "Home", model = modelhientai });
        }

        //Chức năng Sửa thông tin:
        public async Task<IActionResult> Sua(int? id)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            Danhsachlinhkien danhsachlinhkien = await db.Danhsachlinhkiens.FindAsync(id);
            if (danhsachlinhkien == null)
            {
                return NotFound();
            }
            return View(danhsachlinhkien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(int id, [Bind("Id,Model,Tenjig,Majig,Tenlinhkien,Malinhkien,Maker,Donvi,Dongia,Tonkho,Ghichu,Image")] Danhsachlinhkien danhsachlinhkien)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            if (id != danhsachlinhkien.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(danhsachlinhkien);
                    await db.SaveChangesAsync();
                    TempData["OK"] = "1";
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            string modelhientai = TempData["select_model"].ToString();
            TempData.Keep();
            return RedirectToRoute(new { action = "Index", controller = "Home", model = modelhientai });
        }

        //Chức năng Xoá linh kiện:
        public async Task<IActionResult> Xoa(int? id)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            Danhsachlinhkien danhsachlinhkien = await db.Danhsachlinhkiens.FindAsync(id);
            if (danhsachlinhkien == null)
            {
                return NotFound();
            }
            return View(danhsachlinhkien);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            var danhsachlinhkien = await db.Danhsachlinhkiens.FindAsync(id);
            if (danhsachlinhkien != null)
            {
                db.Danhsachlinhkiens.Remove(danhsachlinhkien);
                TempData["OK"] = "1";
            }

            await db.SaveChangesAsync();
            string modelhientai = TempData["select_model"].ToString();
            TempData.Keep();
            return RedirectToRoute(new { action = "Index", controller = "Home", model = modelhientai });
        }

        //Chức năng thông báo lỗi:
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Trang Lịch sử đặt hàng:
        public async Task<IActionResult> Lichsudathang(string model)
        {
            TempData["sldangyeucau"] = db.Dathangs.Where(x => x.Trangthai == "Yêu cầu đặt hàng").Count();
            TempData["sldangdat"] = db.Dathangs.Where(x => (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu")).Count();

            TempData["slIJP"] = db.Danhsachlinhkiens.Where(x => x.Model == "IJP").Count();
            TempData["slhet_IJP"] = db.Danhsachlinhkiens.Where(x => (x.Model == "IJP" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_IJP"] = db.Dathangs.Where(x => (x.Model == "IJP" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_IJP"] = db.Dathangs.Where(x => (x.Model == "IJP" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

            TempData["slLD2"] = db.Danhsachlinhkiens.Where(x => x.Model == "LD2").Count();
            TempData["slhet_LD2"] = db.Danhsachlinhkiens.Where(x => (x.Model == "LD2" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_LD2"] = db.Dathangs.Where(x => (x.Model == "LD2" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_LD2"] = db.Dathangs.Where(x => (x.Model == "LD2" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

            TempData["slhontai"] = db.Danhsachlinhkiens.Where(x => x.Model == "hontai").Count();
            TempData["slhet_hontai"] = db.Danhsachlinhkiens.Where(x => (x.Model == "hontai" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_hontai"] = db.Dathangs.Where(x => (x.Model == "hontai" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_hontai"] = db.Dathangs.Where(x => (x.Model == "hontai" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

            TempData["slfix"] = db.Danhsachlinhkiens.Where(x => x.Model == "fixing").Count();
            TempData["slhet_fix"] = db.Danhsachlinhkiens.Where(x => (x.Model == "fixing" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_fix"] = db.Dathangs.Where(x => (x.Model == "fixing" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_fix"] = db.Dathangs.Where(x => (x.Model == "fixing" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

            TempData["slcsd"] = db.Danhsachlinhkiens.Where(x => x.Model == "csd").Count();
            TempData["slhet_csd"] = db.Danhsachlinhkiens.Where(x => (x.Model == "csd" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_csd"] = db.Dathangs.Where(x => (x.Model == "csd" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_csd"] = db.Dathangs.Where(x => (x.Model == "csd" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

            TempData["slitb"] = db.Danhsachlinhkiens.Where(x => x.Model == "itb-driver").Count();
            TempData["slhet_itb"] = db.Danhsachlinhkiens.Where(x => (x.Model == "itb-driver" && x.Tonkho == 0)).Count();
            TempData["sldangyeucau_itb"] = db.Dathangs.Where(x => (x.Model == "itb-driver" && x.Trangthai == "Yêu cầu đặt hàng")).Count();
            TempData["sldangdat_itb"] = db.Dathangs.Where(x => (x.Model == "itb-driver" && (x.Trangthai == "Đang đặt hàng" || x.Trangthai == "Hàng về thiếu"))).Count();

            ViewBag.tenmodel = new SelectList(db.Models, "Tenmodel", "Tenmodel"); // danh sách
            TempData["select_model"] = model;

            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            var hoten = session.GetString("user");
            if (!String.IsNullOrEmpty(model))
            {
                TempData["this_sldangyeucau"] = db.Dathangs.Where(x => (x.Trangthai == "Yêu cầu đặt hàng" && x.Model == model)).Count();
                TempData["this_sldangdathang"] = db.Dathangs.Where(x => (x.Trangthai == "Đang đặt hàng" && x.Model == model)).Count();
                TempData["this_slhangvethieu"] = db.Dathangs.Where(x => (x.Trangthai == "Hàng về thiếu" && x.Model == model)).Count();

                TempData["this_slhangdave"] = db.Dathangs.Where(x => (x.Trangthai == "Hàng đã về" && x.Model == model)).Count();

                TempData["select_model"] = model;
                TempData["modelduocchon"] = model;
                var dathangs = await db.Dathangs.Where(x => x.Model == model).OrderByDescending(x => x.Ngaydathang).ToListAsync();
                return View(dathangs);
            }
            return View();
        }

        //Chức năng Đặt hàng:
        public async Task<IActionResult> Dathang(int? id)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            List<Danhsachlinhkien> thongtinlinhkien = await db.Danhsachlinhkiens.Where(x => x.Id == id).ToListAsync();
            ViewData["thongtinlinhkien"] = thongtinlinhkien;

            return View();
        }

        [HttpPost]
        public IActionResult Dathang(Dathang dat)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            try
            {
                Dathang dh = new Dathang();
                dh.Ngayyeucau = DateTime.Now.ToString("yyyy/MM/dd");
                dh.Ngaydathang = "";
                dh.Ngayhangve = "";
                dh.Ngaydukienhangve = "";
                dh.Tenjig = dat.Tenjig;
                dh.Majig = dat.Majig;
                dh.Tenlinhkien = dat.Tenlinhkien;
                dh.Malinhkien = dat.Malinhkien;
                dh.Maker = dat.Maker;
                dh.Soluong = dat.Soluong;
                dh.Donvi = dat.Donvi;
                dh.Dongia = dat.Dongia;
                dh.Thanhtien = dh.Soluong * dh.Dongia;
                dh.Ghichu = dat.Ghichu;
                dh.Trangthai = "Yêu cầu đặt hàng";
                dh.Nguoidathang = dat.Nguoidathang;
                dh.Image = dat.Image;
                dh.Model = dat.Model;
                dh.Soluongtonkho = dat.Soluongtonkho;
                db.Dathangs.Add(dh);
                db.SaveChanges();
                TempData["OK"] = "1";
                string modelhientai = TempData["select_model"].ToString();
                TempData.Keep();
                return RedirectToRoute(new { action = "Index", controller = "Home", model = modelhientai });
            }
            catch
            {
                TempData["NG"] = "1";
                string modelhientai = TempData["select_model"].ToString();
                TempData.Keep();
                return RedirectToRoute(new { action = "Index", controller = "Home", model = modelhientai });
            }
        }

        //Chức năng Xác nhận đặt hàng:
        public async Task<IActionResult> Xacnhandathang(int? id)
        {
            Dathang dathang = await db.Dathangs.FindAsync(id);
            return View(dathang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xacnhandathang(int id, [Bind("Id,Ngayyeucau,Ngaydathang,Ngaydukienhangve,Ngayhangve,Tenlinhkien,Malinhkien,Maker,Soluongtonkho,Soluong,Donvi,Dongia,Thanhtien,Ghichu,Trangthai,Nguoidathang,Image,Model")] Dathang dathang)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            if (id != dathang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(dathang);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            string modelhientai = TempData["modelduocchon"].ToString();
            TempData.Keep();
            return RedirectToRoute(new { action = "Lichsudathang", controller = "Home", model = modelhientai });
        }

        //Chức năng Xác nhận hàng về:
        public async Task<IActionResult> Xacnhanhangve(int? id, string trangthaihangve)
        {
            Dathang dathang = await db.Dathangs.FindAsync(id);
            ViewBag.trangthai = trangthaihangve;
            return View(dathang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xacnhanhangve(int id, string trangthaihangve, [Bind("Id,Ngayyeucau,Ngaydathang,Ngayhangve,Ngaydukienhangve,Ngaydukienhangvedot2,Ngayhangvedot2,Tenlinhkien,Malinhkien,Maker,Soluongtonkho,Soluong,Donvi,Dongia,Thanhtien,Ghichu,Trangthai,Nguoidathang,Image,Model,Soluongvedot1,Soluongvedot2")] Dathang dathang)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            if (id != dathang.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (trangthaihangve == "Đủ")
                    {
                        var linhkien = db.Danhsachlinhkiens.Where(s => (s.Tenlinhkien == dathang.Tenlinhkien && s.Malinhkien == dathang.Malinhkien)).FirstOrDefault();
                        if (linhkien != null)
                        {
                            var soluongmoi = linhkien.Tonkho + dathang.Soluongvedot1;
                            linhkien.Tonkho = soluongmoi;
                        }
                        db.Update(dathang);
                        await db.SaveChangesAsync();
                        TempData["OK"] = "1";
                    }
                    if (trangthaihangve == "Thiếu")
                    {
                        var linhkien = db.Danhsachlinhkiens.Where(s => (s.Tenlinhkien == dathang.Tenlinhkien && s.Malinhkien == dathang.Malinhkien)).FirstOrDefault();
                        if (linhkien != null)
                        {
                            var soluongmoi = linhkien.Tonkho + dathang.Soluongvedot1;
                            linhkien.Tonkho = soluongmoi;
                        }
                        db.Update(dathang);
                        await db.SaveChangesAsync();
                        TempData["OK"] = "1";
                    }
                    if (trangthaihangve == "final")
                    {
                        var linhkien = db.Danhsachlinhkiens.Where(s => (s.Tenlinhkien == dathang.Tenlinhkien && s.Malinhkien == dathang.Malinhkien)).FirstOrDefault();
                        if (linhkien != null)
                        {
                            var soluongmoi = linhkien.Tonkho + dathang.Soluongvedot2;
                            linhkien.Tonkho = soluongmoi;
                        }
                        db.Update(dathang);
                        await db.SaveChangesAsync();
                        TempData["OK"] = "1";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            string modelhientai = TempData["modelduocchon"].ToString();
            TempData.Keep();
            return RedirectToRoute(new { action = "Lichsudathang", controller = "Home", model = modelhientai });
        }

        //Chức năng lấy linh kiện:
        public async Task<IActionResult> Laylinhkien(int? id)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            Danhsachlinhkien danhsachlinhkien = await db.Danhsachlinhkiens.FindAsync(id);
            if (danhsachlinhkien == null)
            {
                return NotFound();
            }
            return View(danhsachlinhkien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Laylinhkien(Danhsachlinhkien lichsuLaylinhkien)
        {
            string modelhientai = TempData["select_model"].ToString();
            TempData.Keep();
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            var hoten = session.GetString("user");
            if (ModelState.IsValid)
            {
                try
                {
                    var linhkien = db.Danhsachlinhkiens.Where(x => x.Id == lichsuLaylinhkien.Id).FirstOrDefault();
                    if (linhkien != null)
                    {
                        try
                        {
                            var soluongconlai = linhkien.Tonkho - lichsuLaylinhkien.Tonkho;
                            if (soluongconlai >= 0)
                            {
                                LichsuLaylinhkien lichsu = new LichsuLaylinhkien();
                                lichsu.Tenjig = lichsuLaylinhkien.Tenjig;
                                lichsu.Majig = lichsuLaylinhkien.Majig;
                                lichsu.Tenlinhkien = lichsuLaylinhkien.Tenlinhkien;
                                lichsu.Malinhkien = lichsuLaylinhkien.Malinhkien;
                                lichsu.Maker = lichsuLaylinhkien.Maker;
                                lichsu.Donvi = lichsuLaylinhkien.Donvi;
                                lichsu.Soluong = lichsuLaylinhkien.Tonkho;
                                lichsu.Nguoilay = hoten;
                                lichsu.Ngaylay = DateTime.Now;
                                lichsu.Image = lichsuLaylinhkien.Image;
                                lichsu.Model = lichsuLaylinhkien.Model;
                                db.LichsuLaylinhkiens.Add(lichsu);

                                linhkien.Tonkho = soluongconlai;
                                await db.SaveChangesAsync();
                                TempData["OK"] = "1";

                                return RedirectToRoute(new { action = "Index", controller = "Home", model = modelhientai });
                            }
                            else
                            {
                                TempData.Keep();
                                return RedirectToRoute(new { action = "Index", controller = "Home", model = modelhientai });
                            }
                        }
                        catch
                        {
                            TempData["NG"] = "1";
                            TempData.Keep();
                            return RedirectToRoute(new { action = "Index", controller = "Home", model = modelhientai });
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            TempData.Keep();
            return RedirectToRoute(new { action = "Index", controller = "Home", model = modelhientai });
        }

        //Trang Lịch sử lấy linh kiện:
        public async Task<IActionResult> Lichsulaylinhkien(string model, string ngaybatdau, string ngayketthuc)
        {
            ViewBag.tenmodel = new SelectList(db.Models, "Tenmodel", "Tenmodel"); // danh sách
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            DateTime dateStart = DateTime.Now;
            dateStart = dateStart.ChangeTime(1, 0, 10);

            ViewBag.homnay = db.LichsuLaylinhkiens.Where(x => x.Ngaylay >= dateStart).ToList();
            var dem = db.LichsuLaylinhkiens.Where(s => s.Ngaylay > dateStart).Count();
            TempData["dem"] = dem;
            var demIJP = db.LichsuLaylinhkiens.Where(s => s.Ngaylay > dateStart && s.Model == "IJP").Count();
            TempData["demIJP"] = demIJP;
            var demLD2 = db.LichsuLaylinhkiens.Where(s => s.Ngaylay > dateStart && s.Model == "LD2").Count();
            TempData["demLD2"] = demLD2;
            var demHONTAI = db.LichsuLaylinhkiens.Where(s => s.Ngaylay > dateStart && s.Model == "HONTAI").Count();
            TempData["demHONTAI"] = demHONTAI;
            var demFIXING = db.LichsuLaylinhkiens.Where(s => s.Ngaylay > dateStart && s.Model == "FIXING").Count();
            TempData["demFIXING"] = demFIXING;
            var demCSD = db.LichsuLaylinhkiens.Where(s => s.Ngaylay > dateStart && s.Model == "CSD").Count();
            TempData["demCSD"] = demCSD;
            var demITB = db.LichsuLaylinhkiens.Where(s => s.Ngaylay > dateStart && s.Model == "ITB-DRIVER").Count();
            TempData["demITB"] = demITB;
            var demTOOLS = db.LichsuLaylinhkiens.Where(s => s.Ngaylay > dateStart && s.Model == "TOOLS").Count();
            TempData["demTOOLS"] = demTOOLS;

            var hoten = session.GetString("user");
            if (!String.IsNullOrEmpty(model) && !String.IsNullOrEmpty(ngaybatdau) && !String.IsNullOrEmpty(ngayketthuc)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                TempData["ngaybatdau"] = ngaybatdau;
                TempData["ngayketthuc"] = ngayketthuc;
                TempData["modelchon"] = model;

                DateTime start = DateTime.Parse(ngaybatdau);
                start = start.ChangeTime(1, 0, 10);
                DateTime end = DateTime.Parse(ngayketthuc);
                end = end.ChangeTime(23, 0, 10);

                var list = db.LichsuLaylinhkiens
                           .Where(x => x.Ngaylay >= start && x.Ngaylay <= end && x.Model == model)
                           .ToList();
                return View(list);
            }
            return View();
        }

        //Chức năng Xoá linh kiện:
        public async Task<IActionResult> Xoadathang(int? id)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            Dathang dathang = await db.Dathangs.FindAsync(id);
            if (dathang == null)
            {
                return NotFound();
            }
            return View(dathang);
        }

        [HttpPost, ActionName("Xoadathang")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDathangConfirmed(int id)
        {
            ISession session = HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            var dathang = await db.Dathangs.FindAsync(id);
            if (dathang != null)
            {
                db.Dathangs.Remove(dathang);
                TempData["OK"] = "1";
            }
            await db.SaveChangesAsync();
            string modelhientai = TempData["modelduocchon"].ToString();
            TempData.Keep();
            return RedirectToRoute(new { action = "Lichsudathang", controller = "Home", model = modelhientai });
        }
    }
}