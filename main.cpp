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

// ������������ ������������� ���������
#define CERT_STORE_NAME  L"MY"

// ������������ �����������, �������������� � ��� ���������
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

	// ���� �� ������������� ����� �����������.
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
		//  ������������� ������ � ������ ��� �������������� ����� �����.
		pszName = (LPTSTR) malloc(cbName);
		if(!pszName)
			puts("ERROR - malloc failed!");
		memset(pszName, 0, cbName);
		//  ��������� ����� ���� ����������.
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
	//   ������ ��������� ������������ �����������.
	printf("\n\n          Listing Available Providers.\n");
	printf("Provider type      Provider Name\n");
	printf("_______________________________________\n");   

	// ���� �� ������������� �����������.
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
		//  ������������� ������ � ������ ��� �������������� ����� �����.
		pszName = (LPTSTR)malloc(cbName);
		if(!pszName)
			puts("ERROR - malloc failed!");
		memset(pszName, 0, cbName);
		//  ��������� ����� ����������.
		if(CryptEnumProviders(
			dwIndex++,
			NULL,
			0,
			&dwType,
			(LPWSTR)pszName,
			&cbName     // pcbProvName -- ����� pszName
			))
		{   
			printf ("     %4.0d        ",dwType);
			wcout<<pszName<<endl;
		}
		else
		{
			puts("ERROR - CryptEnumProviders");
		}
	} // ����� ����� while
	printf("\nProvider types and provider names have been listed.\n");
	///////////////////////////////////////////////////////////////////////////////////////
	//�������� ������� ������������ keyset ��� ������� �����
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
 
 // ��������� ��������� ������������
	HCERTSTORE hStore;    
	

	if ( !( hStore = CertOpenStore(
		CERT_STORE_PROV_SYSTEM,
		0,
		NULL,
		CERT_SYSTEM_STORE_CURRENT_USER,
		CERT_STORE_NAME)))
	{
		printf("������ ������� ��������� MY ");

	}

	// �������� ��������� �� ��� ����������
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

	//� ������� ������ ������� ������� ��� CSP � ��� ���������� ������
	DWORD dwUserNameLen = 100;    
	CHAR szUserName[100];

	if(CryptGetProvParam(
		hProv,              // ���������� �� CSP
		PP_NAME,            // �������� ��� ��������� ����� CSP
		(BYTE *)szUserName, // ��������� �� �����, ���������� ��� CSP
		&dwUserNameLen,     // ����� ������ 
		0)) 
	{
		printf("Name CSP: %s\n",szUserName);
	}
	else
	{
		puts("������ CryptGetProvParam.\n");
	}

	if(CryptGetProvParam(
		hProv,              // ���������� �� CSP
		PP_CONTAINER,      // �������� ��� ��������� ����� key container  
		(BYTE *)szUserName, // ��������� �� �����, ���������� ��� key container
		&dwUserNameLen,     // ����� ������
		0)) 
	{
		printf("Name key container: %s\n",szUserName);
	}
	else
	{
		puts("ErrorCryptGetProvParam.\n");  
	}
	HCRYPTKEY hSessionKey;

	// ��������� ����������� �����
if (!CryptGenKey(hProv, CALG_DES, 
    CRYPT_ENCRYPT | CRYPT_DECRYPT, &hSessionKey))
{
  printf("CryptGenKey");
  return;
}

std::cout << "Session key generated" << std::endl;

// ������ ��� ����������
char string[]="Test ";
DWORD count=strlen(string);

// ���������� ������
if (!CryptEncrypt(hSessionKey, 0, true, 0, (BYTE*)string, 
    &count, strlen(string)))
{
  printf("CryptEncrypt");
  return;
}

std::cout << "Encryption completed" << std::endl;

// �������� ����� �� �����
std::cout << "Encrypted string: " << string << std::endl;


// ��������������� ������
if(!CryptDecrypt(hSessionKey, 0, true, 0, (BYTE*)string, &count))
{
 printf("CryptDecrypt");
  return;
}

std::cout << "Decryption completed" << std::endl;

 // �������� ����� �� �����
std::cout << "Decrypted string: " << string << std::endl;

 // ������������ ��������� ��������� ����������
CryptDestroyKey(hSessionKey); 
CryptReleaseContext(hProv, 0);

_getch();
}