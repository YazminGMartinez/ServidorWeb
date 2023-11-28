using System.Net.Mail;
public class MensajeCorreo{
    public  string Destinatario{get;set;}="";
    public string Asunto {get;set;}="";
    public string Mensaje {get;set;}="";
    public void Enviar(){
        MailMessage correo = new MailMessage();
    correo.From = new MailAddress("gonzalezmartinez.valeria.cb105@gmail.com", null, System.Text.Encoding.UTF8);
    correo.To.Add(this.Destinatario);
    correo.Subject = this.Asunto;
    correo.Body = this.Mensaje; 
    correo.IsBodyHtml = true;
    correo.Priority = MailPriority.Normal;
    SmtpClient smtp = new SmtpClient();
    smtp.UseDefaultCredentials = false;
    smtp.Host = "smtp.gmail.com"; 
    smtp.Port = 25; 
    smtp.EnableSsl=true;
    smtp.Credentials = new System.Net.NetworkCredential("gonzalezmartinez.valeria.cb105@gmail.com", "qjrx ngvr ousp cuuh");
    smtp.Send(correo);
    }
}