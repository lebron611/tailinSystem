#!/usr/bin/python
import smtplib,ssl
import getpass, poplib
from email.mime.multipart import MIMEMultipart
from email.mime.base import MIMEBase
from email.mime.text import MIMEText
from email.utils import formatdate
from email import encoders
from pathlib import Path

def send_mail(send_from,send_to,subject,text,files,server,port,username,password,isTls=True):
    print(username)
    print(password)
    print(files)
    msg = MIMEMultipart()
    msg['From'] = send_from
    msg['To'] = send_to
    msg['Date'] = formatdate(localtime = True)
    msg['Subject'] = subject
    msg.attach(MIMEText(text))

    part = MIMEBase('application', "octet-stream")
    part.set_payload(open(files, "rb").read())
    encoders.encode_base64(part)
    part.add_header('Content-Disposition', 'attachment; filename="Excel.xlsx')
    msg.attach(part)

    #context = ssl.SSLContext(ssl.PROTOCOL_SSLv3)
    #SSL connection only working on Python 3+
    smtp = smtplib.SMTP(server, port)

    smtp.login(username,password)
    smtp.sendmail(send_from, send_to, msg.as_string())
    smtp.quit()
    
if __name__ == "__main__":
    home = str(Path.home())
    send_from='dl2221@tailyn.com.tw'
    send_to='lebron611@hotmail.com'
    subject='Subject1234'
    text='content text'
    files=home + "/Desktop/派工報表V.xlsx"
    server='smtp.tailyn.com.tw'
    port=25
    username='dl2221@tailyn.com.tw'
    password='19850927'
    send_mail(send_from,send_to,subject,text,files,server,port,username,password,isTls=True)
