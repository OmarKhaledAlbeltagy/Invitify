using Invitify.Context;
using Invitify.Entities;
using Invitify.Models;
using Microsoft.EntityFrameworkCore.Storage;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace Invitify.Repos
{
    public class PropertiesRep:IPropertiesRep
    {
        private readonly DbContainer db;

        public PropertiesRep(DbContainer db)
        {
            this.db = db;
        }

        public bool AddLogo(AddLogoModel obj)
        {
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    List<Logo> del = db.logo.ToList();

                    if (del.Count > 0)
                    {
                        db.logo.RemoveRange(del);
                        db.SaveChanges();
                    }

                 

                    for (int i = 0; i < 2; i++)
                    {
                        int indx = obj.file[i].FileName.Split('.').Length - 1;
                        string extension = obj.file[i].FileName.Split('.')[indx];
                        string fileType = obj.file[i].ContentType;
                        using (var stream = new MemoryStream())
                        {
                            obj.file[i].CopyTo(stream);
                            var bytes = stream.ToArray();
                            Logo l = new Logo();
                            l.Extension = extension;
                            l.ContentType = fileType;
                            l.Data = bytes;
                            l.Description = obj.Description[i];
                            db.logo.Add(l);
                        }
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return true;

                }
                catch (Exception ex)
                {

                    string m = ex.Message;
                    transaction.Rollback();
                    return false;
                }
            }


        }

        public bool CheckLogo()
        {
            List<Logo> del = db.logo.ToList();

            if (del.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditProperty(EditPropertyModel obj)
        {
            Properties p = db.properties.Find(obj.Id);
            p.Value = obj.Value;
            db.SaveChanges();
            return true;
        }

        public List<Properties> GetAllProperties()
        {
            List<Properties> res = db.properties.ToList();
            return res;
        }

        public bool MigrationQrCodes()
        {
            List<Invitees> inv = db.invitees.ToList();

            Logo l = db.logo.Where(a => a.Description == "White").FirstOrDefault();

            if (l != null && inv.Count > 0)
            {

                using (IDbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in inv)
                        {
                            Eventt ev = db.eventt.Find(item.eventtId);


                                var logoStream = new MemoryStream(l.Data);
                            
                                var qrGenerator = new QRCodeGenerator();
                                var qrCodeData = qrGenerator.CreateQrCode(ev.Domain + "?" + ev.Guidd + "&" + db.contact.Find(item.ContactId).Guidd, QRCodeGenerator.ECCLevel.H);
                                var qrCodeBitmap = new QRCode(qrCodeData).GetGraphic(60);
                                var logoImage = Image.FromStream(logoStream);
                                var logoWidth = qrCodeBitmap.Width / 4;
                                var logoHeight = qrCodeBitmap.Height / 4;
                                var logoResized = new Bitmap(logoImage, logoWidth, logoHeight);

                                var logoX = (qrCodeBitmap.Width - logoWidth) / 2;
                                var logoY = (qrCodeBitmap.Height - logoHeight) / 2;

                                using var graphics = Graphics.FromImage(qrCodeBitmap);
                                graphics.DrawImage(logoResized, logoX, logoY, logoWidth, logoHeight);

                                using (var stream = new MemoryStream())
                                {
                                    Invitees invv = db.invitees.Find(item.Id);
                                    qrCodeBitmap.Save(stream, ImageFormat.Png);
                                    var bytes = stream.ToArray();
                                    item.Data = bytes;
                                    item.IsSms = true;
                                }
                            
                            
                        }
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {

                        string m = ex.Message;
                        transaction.Rollback();
                        return false;
                    }
                }




                }

            else
            {
                return true;
            }
        }
    }
}
