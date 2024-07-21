using Invitify.Context;
using Invitify.Entities;
using Invitify.Models;
using Invitify.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;

namespace Invitify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationRep rep;
        private readonly DbContainer db;

        public InvitationController(IInvitationRep rep, DbContainer db)
        {
            this.rep = rep;
            this.db = db;
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult DownloadBulkQrs(List<int> ids)
        {

            Eventt ev = db.eventt.Find(db.invitees.Find(ids[0]).eventtId);
            using (var compressedFileStream = new MemoryStream())
            {
                //Create an archive and store the stream in memory.
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                {
                    foreach (var caseAttachmentModel in ids)
                    {
                        Invitees inv = db.invitees.Find(caseAttachmentModel);
                        //Create a zip entry for each attachment
                        var zipEntry = zipArchive.CreateEntry(db.contact.Find(inv.ContactId).ContactName + " - " + ev.EventName + ".png");

                        //Get the stream of the attachment
                        using (var originalFileStream = new MemoryStream(inv.Data))
                        using (var zipEntryStream = zipEntry.Open())
                        {
                            //Copy the attachment stream to the zip entry stream
                            originalFileStream.CopyTo(zipEntryStream);
                        }
                    }
                }

                return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = ev.EventName + ".zip" };
            }
        }



        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetEventInvitees(int id)
        {
            return Ok(rep.GetEventInvitees(id));
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult DownloadEventQrs(int id)
        {
            List<Invitees> invs = db.invitees.Where(a=>a.eventtId == id).ToList();
            Eventt ev = db.eventt.Find(id);
            using (var compressedFileStream = new MemoryStream())
            {
                //Create an archive and store the stream in memory.
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                {
                    foreach (var caseAttachmentModel in invs)
                    {
                        //Create a zip entry for each attachment
                        var zipEntry = zipArchive.CreateEntry(db.contact.Find(caseAttachmentModel.ContactId).ContactName + " - " + ev.EventName + ".png");

                        //Get the stream of the attachment
                        using (var originalFileStream = new MemoryStream(caseAttachmentModel.Data))
                        using (var zipEntryStream = zipEntry.Open())
                        {
                            //Copy the attachment stream to the zip entry stream
                            originalFileStream.CopyTo(zipEntryStream);
                        }
                    }
                    ZipArchiveEntry zipEntryy;
                    if (ev.AllowAnonymous)
                    {
                       zipEntryy = zipArchive.CreateEntry("Anonymous Registration QR Code/Anonymous Registration - " + ev.EventName + ".png");
                    }
                    else
                    {
                        zipEntryy = zipArchive.CreateEntry("Main Page QR Code/Main Page - " + ev.EventName + ".png");
                    }
                    var qrGenerator = new QRCodeGenerator();
                        var qrCodeData = qrGenerator.CreateQrCode(ev.Domain + "?" + ev.Guidd, QRCodeGenerator.ECCLevel.H);
                        var qrCodeBitmap = new QRCode(qrCodeData).GetGraphic(60);
                        Logo l = db.logo.Where(a => a.Description == "White").FirstOrDefault();
                        if (l != null)
                        {
                            var logostream = new MemoryStream(l.Data);
                            var logoImage = Image.FromStream(logostream);
                            var logoWidth = qrCodeBitmap.Width / 4;
                            var logoHeight = qrCodeBitmap.Height / 4;
                            var logoResized = new Bitmap(logoImage, logoWidth, logoHeight);

                            var logoX = (qrCodeBitmap.Width - logoWidth) / 2;
                            var logoY = (qrCodeBitmap.Height - logoHeight) / 2;

                            using var graphics = Graphics.FromImage(qrCodeBitmap);
                            graphics.DrawImage(logoResized, logoX, logoY, logoWidth, logoHeight);
                        }
                        var streamm = new MemoryStream();
                        qrCodeBitmap.Save(streamm, ImageFormat.Png);

                        using (var originalFileStream = new MemoryStream(streamm.ToArray()))
                        {
                          
                            using (var zipEntryStream = zipEntryy.Open())
                            {
                                //Copy the attachment stream to the zip entry stream
                                originalFileStream.CopyTo(zipEntryStream);
                            }
                        }
                   
                    
                }

                return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = ev.EventName+".zip" };
            }
        }


        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetInviteesData()
        {
            return Ok(rep.GetInviteesData());
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddToInvitees(AddToInviteesModel obj)
        {
            return Ok(rep.AddToInvitees(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult RemoveFromInvitees(AddToInviteesModel obj)
        {
            return Ok(rep.RemoveFromInvitees(obj));
        }

    }
}
