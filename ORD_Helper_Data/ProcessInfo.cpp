#include "pch.h"
#include "ProcessInfo.h"

namespace ORD_Helper_Data
{
	Process::Process()
		:_processId(0), _processState(0)
	{
	}

	ProcessHandler::ProcessHandler()
	{
		out = new std::ofstream();
		in = new std::ifstream();
		process = gcnew Process();
	}

	ProcessHandler::~ProcessHandler()
	{
		delete out;
		delete in;
		delete process;
	}

	void ProcessHandler::Copy(ProcessHandler ^ temp)
	{
		this->process->address = temp->process->address;
		this->process->processH = temp->process->processH;
		this->process->processId = temp->process->processId;
		this->process->processState = temp->process->processState;
	}

	int ProcessHandler::SetProcessId(const wchar_t * targetProcess)
	{
		HANDLE snap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
		if (snap && snap != INVALID_HANDLE_VALUE)
		{
			PROCESSENTRY32 pe;
			pe.dwSize = sizeof(pe);
			if (Process32First(snap, &pe))
			{
				do
				{
					if (!wcscmp(pe.szExeFile, targetProcess))
					{
						CloseHandle(snap);
						process->processId = pe.th32ProcessID;
						return 1;
					}
				} while (Process32Next(snap, &pe));
			}
		}
		return 0;
	}


	char * ProcessHandler::GetModuleBase(const wchar_t * ModuleName, DWORD procID)
	{
		MODULEENTRY32 ModuleEntry = { 0 };
		HANDLE SnapShot = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE | TH32CS_SNAPMODULE32, procID);

		if (!SnapShot) return NULL;

		ModuleEntry.dwSize = sizeof(ModuleEntry);

		if (!Module32First(SnapShot, &ModuleEntry)) return NULL;

		do
		{
			if (!wcscmp(ModuleEntry.szModule, ModuleName))
			{
				CloseHandle(SnapShot);
				return (char*)ModuleEntry.modBaseAddr;
			}
		} while (Module32Next(SnapShot, &ModuleEntry));

		CloseHandle(SnapShot);
		return NULL;
	}


	void ProcessHandler::CheckProcessInfo()
	{
		process->processState = 0;
		if (SetProcessId(process->address) == 1)
		{
			process->processState = 1;
		}

		process->processH = OpenProcess(PROCESS_ALL_ACCESS, FALSE, process->processId);
		if (process->processH == INVALID_HANDLE_VALUE)
		{
			process->processState = 2;
		}
	}

	void ProcessHandler::SaveProcess(char * temp)
	{
		char * directory = new char[strlen("Version/") + strlen(temp) + strlen("/ORD_Helper_Data.Process.bin") + 1];
		strcpy_s(directory, strlen("Version/") + 1, "Version/");
		strcat_s(directory, strlen("Version/") + strlen(temp) + 1, temp);
		strcat_s(directory, strlen("Version/") + strlen(temp) + strlen("/ORD_Helper_Data.Process.bin") + 1, "/ORD_Helper_Data.Process.bin");
		out->open(directory, std::ios::binary);

		if (out->is_open())
		{
			Save(process->addressLength);
			Save(process->address);
			
			out->close();
		}
	}

	template <typename T>
	void ProcessHandler::Save(T temp)
	{
		if (std::is_same<T, wchar_t *>::value)
			out->write((const char*)temp, (wcslen((wchar_t*)temp) + 1) * 2);
		else
			out->write((const char*)&temp, sizeof(T));
	}

	void ProcessHandler::LoadProcess(char * temp)
	{
		char * directory = new char[strlen("Version/") + strlen(temp) + strlen("/ORD_Helper_Data.Process.bin") + 1];
		strcpy_s(directory, strlen("Version/") + 1, "Version/");
		strcat_s(directory, strlen("Version/") + strlen(temp) + 1, temp);
		strcat_s(directory, strlen("Version/") + strlen(temp) + strlen("/ORD_Helper_Data.Process.bin") + 1, "/ORD_Helper_Data.Process.bin");
		in->open(directory, std::ios::binary);

		if (in->is_open())
		{
			process->addressLength = Load<int>();
			process->address = Load<wchar_t>(process->addressLength);

			in->close();
		}
	}

	template <typename T>
	T ProcessHandler::Load()
	{
		T temp;
		in->read((char*)&temp, sizeof(T));
		return temp;
	}

	template <typename T>
	T * ProcessHandler::Load(int size)
	{
		if (std::is_same<T, wchar_t>::value)
		{
			wchar_t * temp = new wchar_t[size];
			in->read((char*)temp, size * 2);

			return temp;
		}
		else
		{
			T * temp = new T[size];
			in->read((char*)temp, size);
			return temp;
		}
	}

	void ProcessHandler::SetAddress(char * temp)
	{
		wchar_t * temp2 = new wchar_t[strlen(temp) + 1];
		mbstowcs_s(NULL, temp2, strlen(temp) + 1, temp, strlen(temp) + 1);
		process->address = temp2;
	}
}