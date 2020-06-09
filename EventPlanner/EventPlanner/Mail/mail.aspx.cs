using System.Net;
using System.Net.Mail;
using DnsClient;
public partial class mail 
{
  public void SendMail()
    {
		MailAddress to = new MailAddress("dtlgeakzozqntccjdx@ttirv.com");
		MailAddress from = new MailAddress("piotr@mailtrap.com");

		MailMessage message = new MailMessage(from, to);
		message.Subject = "See you Monday?";
		message.Body = "Elizabeth, I didn't hear ";

		LookupClient lookup = new LookupClient();
		IDnsQueryResponse response = lookup.Query("westminster.co.uk", QueryType.MX);

		foreach (DnsClient.Protocol.MxRecord record in response.Answers)
		{
			//Console.WriteLine(ObjectDumper.Dump(record.Exchange));

			SmtpClient client = new SmtpClient(record.Exchange, 25);
			try
			{
				client.Send(message);
				// if we reached this point, our email was sent and we can break the loop
				break;
			}
			catch (SmtpException ex)
			{
				//Console.WriteLine(ex.ToString());
			}
		}
	}
}