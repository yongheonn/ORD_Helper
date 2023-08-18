#include "pch.h"
#include "Error.h"
using namespace std;

namespace ORD_Helper_Data
{
	void ErrorMessage(int _errorCode, char * _errorMessage)
	{
		time_t t = time(NULL);
		struct tm tm = *localtime(&t);
		ofstream out;
		out.open("error_log.txt", ios::app);
		if (_errorCode <= 0)
		{
			_errorCode = 1;		//eCode 1
			strcpy_s(_errorMessage, sizeof("ErrorMessage _errorCode 오류"), "ErrorMessage _errorCode 오류");
			if (out.is_open())
			{
				out << tm.tm_year + 1900 << "-" << tm.tm_mon + 1 << "-" << tm.tm_mday << " ";
				out << tm.tm_hour << ":" << tm.tm_min << ":" << tm.tm_sec << "  ";
				out << "Code:" << _errorCode << " " << _errorMessage << endl;
			}
		}
		if (_errorMessage == NULL)
		{
			_errorCode = 2;		//eCode 2
			strcpy_s(_errorMessage, sizeof("ErrorMessage _errorMessage 오류"), "ErrorMessage _errorMessage 오류");
			if (out.is_open())
			{
				out << tm.tm_year + 1900 << "-" << tm.tm_mon + 1 << "-" << tm.tm_mday << " ";
				out << tm.tm_hour << ":" << tm.tm_min << ":" << tm.tm_sec << "  ";
				out << "Code:" << _errorCode << " " << _errorMessage << endl;
			}
		}
		if (_errorCode > 0 && _errorMessage != NULL)
		{
			if (out.is_open())
			{
				
				out << tm.tm_year + 1900 << "-" << tm.tm_mon + 1 << "-" << tm.tm_mday << " ";
				out << tm.tm_hour << ":" << tm.tm_min << ":" << tm.tm_sec << "  ";
				out << "Code:" << _errorCode << " " << _errorMessage << endl;
			}
		}
		out.close();
	}
}