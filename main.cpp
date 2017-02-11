//#include "main.h"
#pragma comment(lib, "crypt32.lib")
// Link with the Advapi32.lib file.
#pragma comment (lib, "advapi32")


#include <stdio.h>
#include <Windows.h>
#include <WinCrypt.h>
#include <stdlib.h> 
#include <WTypes.h>
#include <iostream>
#include <fstream>
#include <tchar.h>
#include <conio.h>
using namespace std;

#define MY_TYPE  (PKCS_7_ASN_ENCODING | X509_ASN_ENCODING)

// Наименование персонального хранилища
#define CERT_STORE_NAME  L"MY"

// Наименование сертификата, установленного в это хранилище
#define SIGNER_NAME  L"iv"

#define KEYLENGTH  0x00800000
#define ENCRYPT_BLOCK_SIZE 20 




void main()
{
	HCRYPTPROV hProv = NULL;
	LPTSTR      pszName = NULL;
	//char* pszName=NULL;

	DWORD       dwType;
	DWORD       cbName;
	DWORD       dwIndex = 0;

	// Цикл по перечисляемым типам провайдеров.
	dwIndex = 0;
	while(CryptEnumProviderTypes(
		dwIndex,     
		NULL,        
		0,           
		&dwType,     
		NULL,    
		&cbName      
		))
	{ 
		//  Распределение памяти в буфере для восстановления этого имени.
		pszName = (LPTSTR) malloc(cbName);
		if(!pszName)
			puts("ERROR - malloc failed!");
		memset(pszName, 0, cbName);
		//  Получение имени типа провайдера.
		if(CryptEnumProviderTypes(
			dwIndex++,
			NULL,
			0,
			&dwType,   
			pszName,
			&cbName))     
		{
			printf ("   %4.0d ",dwType);
			wcout<<pszName<<endl;
		}
		else
		{
			puts("ERROR - CryptEnumProviders");
		}
	}
	//   Печать заголовка перечисления провайдеров.
	printf("\n\n          Listing Available Providers.\n");
	printf("Provider type      Provider Name\n");
	printf("_______________________________________\n");   

	// Цикл по перечисляемым провайдерам.
	dwIndex = 0;
	while(CryptEnumProviders(
		dwIndex,     
		NULL,        
		0,           
		&dwType,     
		NULL,        
		&cbName      
		))
	{
		//  Распределение памяти в буфере для восстановления этого имени.
		pszName = (LPTSTR)malloc(cbName);
		if(!pszName)
			puts("ERROR - malloc failed!");
		memset(pszName, 0, cbName);
		//  Получение имени провайдера.
		if(CryptEnumProviders(
			dwIndex++,
			NULL,
			0,
			&dwType,
			(LPWSTR)pszName,
			&cbName     // pcbProvName -- длина pszName
			))
		{   
			printf ("     %4.0d        ",dwType);
			wcout<<pszName<<endl;
		}
		else
		{
			puts("ERROR - CryptEnumProviders");
		}
	} // Конец цикла while
	printf("\nProvider types and provider names have been listed.\n");
	///////////////////////////////////////////////////////////////////////////////////////
	//пытаемся открыть существующий keyset или создать новый
	if(!CryptAcquireContextW(&hProv, NULL, 0,PROV_RSA_FULL,0) && 
		!CryptAcquireContextW(&hProv,NULL,0,PROV_RSA_FULL,CRYPT_NEWKEYSET)) 
	{
		puts("NO create keyset\n");
		//return 1;
	}
	else
	{
		puts("YES, create keyset\n");
	}
	
	 HCERTSTORE hStoreHandle;     

  if ( !( hStoreHandle = CertOpenStore(
    CERT_STORE_PROV_SYSTEM,
    0,
    NULL,
    CERT_SYSTEM_STORE_CURRENT_USER,
    //CERT_SYSTEM_STORE_LOCAL_MACHINE,
    CERT_STORE_NAME)))
  {
  printf("no open MY.");

    
  }
  else
  {
   printf("Open MY\n");
   }
 
 // Открываем хранилище сертификатов
	HCERTSTORE hStore;    
	

	if ( !( hStore = CertOpenStore(
		CERT_STORE_PROV_SYSTEM,
		0,
		NULL,
		CERT_SYSTEM_STORE_CURRENT_USER,
		CERT_STORE_NAME)))
	{
		printf("Нельзя открыть хранилище MY ");

	}

	// Получаем указатель на наш сертификат
	PCCERT_CONTEXT pSignerCert=0; 

	if(pSignerCert = CertFindCertificateInStore(
		hStore,
		MY_TYPE,
		0,
		CERT_FIND_SUBJECT_STR,
		SIGNER_NAME,
		NULL))
	{
		printf("Yahhoo, Google, Yandex, Rambler!!!,Sertificate was found !!!!!\n");
	}
	else
	{

		printf("Sertificate was NOT found!!!\n.");
	}

	//С помощью данной функции выведем имя CSP и имя контейнера ключей
	DWORD dwUserNameLen = 100;    
	CHAR szUserName[100];

	if(CryptGetProvParam(
		hProv,              // Дескриптор на CSP
		PP_NAME,            // параметр для получения имени CSP
		(BYTE *)szUserName, // Указатель на буфер, содержащий имя CSP
		&dwUserNameLen,     // длина буфера 
		0)) 
	{
		printf("Name CSP: %s\n",szUserName);
	}
	else
	{
		puts("Ошибка CryptGetProvParam.\n");
	}

	if(CryptGetProvParam(
		hProv,              // Дескриптор на CSP
		PP_CONTAINER,      // параметр для получения имени key container  
		(BYTE *)szUserName, // Указатель на буфер, содержащий имя key container
		&dwUserNameLen,     // длина буфера
		0)) 
	{
		printf("Name key container: %s\n",szUserName);
	}
	else
	{
		puts("ErrorCryptGetProvParam.\n");  
	}
	HCRYPTKEY hSessionKey;

	// Генерация сессионного ключа
if (!CryptGenKey(hProv, CALG_DES, 
    CRYPT_ENCRYPT | CRYPT_DECRYPT, &hSessionKey))
{
  printf("CryptGenKey");
  return;
}

std::cout << "Session key generated" << std::endl;

// Данные для шифрования
char string[]="Test ";
DWORD count=strlen(string);

// Шифрование данных
if (!CryptEncrypt(hSessionKey, 0, true, 0, (BYTE*)string, 
    &count, strlen(string)))
{
  printf("CryptEncrypt");
  return;
}

std::cout << "Encryption completed" << std::endl;

// Тестовый вывод на экран
std::cout << "Encrypted string: " << string << std::endl;


// Расшифровывание данных
if(!CryptDecrypt(hSessionKey, 0, true, 0, (BYTE*)string, &count))
{
 printf("CryptDecrypt");
  return;
}

std::cout << "Decryption completed" << std::endl;

 // Тестовый вывод на экран
std::cout << "Decrypted string: " << string << std::endl;

 // Освобождение контекста локальных переменных
CryptDestroyKey(hSessionKey); 
CryptReleaseContext(hProv, 0);

_getch();
}