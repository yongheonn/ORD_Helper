#include "pch.h"
#include "Resources.h"

namespace ORD_Helper_Data
{
	Resources::Resources()
	{
	}

	ResourcesHandler::ResourcesHandler()
	{
		resources = gcnew Resources();
		in = new std::ifstream();
		out = new std::ofstream();
	}

	void ResourcesHandler::Copy(ResourcesHandler ^ temp)
	{
		this->resources->gold = temp->resources->gold;
		this->resources->goldAddress = temp->resources->goldAddress;
		this->resources->tree = temp->resources->tree;
		this->resources->treeAddress = temp->resources->treeAddress;
	}

	void ResourcesHandler::SearchResources(ProcessHandler ^ ph, GameStateHandler ^ gh)
	{
		DWORD value;
		ReadProcessMemory(ph->process->processH, (char *)resources->goldAddress, &value, sizeof(DWORD), NULL);
		resources->gold = value;

		ReadProcessMemory(ph->process->processH, (char *)resources->treeAddress, &value, sizeof(DWORD), NULL);
		resources->tree = value;
	}

	template <typename T>
	void ResourcesHandler::Save(T temp)
	{
		if (std::is_same<T, char*>::value)
			out->write((const char*)temp, strlen((char*)temp) + 1);
		else
			out->write((const char*)&temp, sizeof(T));
	}

	template <typename T>
	T ResourcesHandler::Load()
	{
		T temp;
		in->read((char*)&temp, sizeof(T));
		return temp;
	}

	char * ResourcesHandler::Load(int size)
	{
		char * temp = new char[size];
		in->read((char*)temp, size);
		return temp;
	}

	void ResourcesHandler::SaveResources()
	{
	//	out->open("ORD_Helper_Data.Resources.bin", std::ios::binary);
	//	if (out->is_open())
	//	{
	//	}
	}

	void ResourcesHandler::LoadResources()
	{
	//	in->open("ORD_Helper_Data.Resources.bin", std::ios::binary);
	//	if (in->is_open())
		{
		}
	}
}