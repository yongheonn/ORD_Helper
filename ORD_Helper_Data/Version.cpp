#include "pch.h"
#include "Version.h"

namespace ORD_Helper_Data
{
	VersionHandler::VersionHandler()
	{
		_ver = gcnew cliext::vector<Version^>();
		out = new std::ofstream();
		in = new std::ifstream();
		verNum = 0;
	}

	Version ^ VersionHandler::ver(int index)
	{
		return _ver[index];
	}

	void VersionHandler::ResizeVer(int size)
	{
		int tempSize = _ver.size();

		_ver.resize(size);
		verNum = size;
		for (int n = tempSize; n < size; n++)
			_ver[n] = gcnew Version();

	}

	void VersionHandler::RemoveVer(int index)
	{
		_ver.erase(_ver.begin() + index);
		verNum--;
	}

	template <typename T>
	void VersionHandler::Save(T temp)
	{
		if (std::is_same<T, char*>::value)
			out->write((const char*)temp, strlen((char*)temp) + 1);
		else
			out->write((const char*)&temp, sizeof(T));
	}

	template <typename T>
	T VersionHandler::Load()
	{
		T temp;
		in->read((char*)&temp, sizeof(T));
		return temp;
	}

	char * VersionHandler::Load(int size)
	{
		char * temp = new char[size];
		in->read((char*)temp, size);
		return temp;
	}

	void VersionHandler::SaveVerInfo()
	{
		out->open("ORD_Helper_Data.Version.bin", std::ios::binary);
		if (out->is_open())
		{
			Save(verNum);
			for (int n = 0; n < verNum; n++)
			{
				Save(_ver[n]->verSize);
				Save(_ver[n]->verName);
			}
			out->close();
		}
	}

	void VersionHandler::LoadVerInfo()
	{
		in->open("ORD_Helper_Data.Version.bin", std::ios::binary);
		if (in->is_open())
		{
			verNum = Load<int>();
			ResizeVer(verNum);
			for (int n = 0; n < verNum; n++)
			{
				_ver[n]->verSize = Load<int>();
				_ver[n]->verName = Load(_ver[n]->verSize);
			}
			in->close();
		}
	}
}