
Create proc NotListesi
As
select NOTID, AD + ' ' + SOYAD As 'AD-SOYAD',SINAV1,SINAV2,SINAV3,ORTALAMA,DURUM from NOTLAR
inner join Ogrenci on NOTLAR.OGR = Ogrenci.ID
inner join DERSLER on DERSLER.DERSID = NOTLAR.DERS