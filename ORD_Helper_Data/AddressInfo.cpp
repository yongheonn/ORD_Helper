#include "pch.h"
#include "AddressInfo.h"

namespace ORD_Helper_Data
{
	Address::Address()
		:_addressSize(0), _offsetNum(0)
	{
		_offset = gcnew cliext::vector<DWORD>();
	}

	Address::~Address()
	{
		delete[]_address;
		delete[]_offset;
	}

	void Address::SetOffset(DWORD value, int index)
	{
		_offset[index] = value;
	}

	DWORD Address::GetOffset(int index)
	{
		return _offset[index];
	}

	void Address::ResizeOffset(int size)
	{
		_offset.resize(size);
		offsetNum = size;
	}

	void Address::RemoveOffset(int index)
	{
		_offset.erase(_offset.begin() + index);
		offsetNum--;
	}

	UnitAddress::UnitAddress()
		:_pointerAddress(0), _checkAddress(0), check(false)
	{
	}

	generic <typename classType>
	AddressHandler<classType>::AddressHandler()
	{
		out = new std::ofstream();
		in = new std::ifstream();
		addressInfo = gcnew classType();
	}

	generic <typename classType>
	AddressHandler<classType>::~AddressHandler()
	{
		delete out;
		delete in;
		delete addressInfo;
	}

	generic <typename classType>
	void AddressHandler<classType>::SetAddress(char * temp)
	{
		wchar_t * temp2 = new wchar_t[strlen(temp) + 1];
		mbstowcs_s(NULL, temp2, strlen(temp) + 1, temp, strlen(temp) + 1);
		addressInfo->address = temp2;
	}

	generic <typename classType>
	void AddressHandler<classType>::SaveAddress(char * temp)
	{
		char * directory = new char[strlen("Version/") + strlen(temp) + strlen((char*)(void*)Marshal::StringToHGlobalAnsi("/" + addressInfo->GetType()->ToString() + ".bin")) + 1];
		strcpy_s(directory, strlen("Version/") + 1, "Version/");
		strcat_s(directory, strlen("Version/") + strlen(temp) + 1, temp);
		strcat_s(directory, strlen("Version/") + strlen(temp) + strlen((char*)(void*)Marshal::StringToHGlobalAnsi("/" + addressInfo->GetType()->ToString() + ".bin")) + 1, (char*)(void*)Marshal::StringToHGlobalAnsi("/" + addressInfo->GetType()->ToString() + ".bin"));
		out->open(directory, std::ios::binary);
		if (out->is_open())
		{
			Save(addressInfo->addressSize, sizeof(int));
			Save((char*)addressInfo->address, addressInfo->addressSize * 2);
			Save(addressInfo->offsetNum, sizeof(int));
			for (int n = 0; n < addressInfo->offsetNum; n++)
				Save(addressInfo->GetOffset(n), sizeof(DWORD));
			out->close();
		}
	}

	generic <typename classType>
	void AddressHandler<classType>::LoadAddress(char * temp)
	{
		char * directory = new char[strlen("Version/") + strlen(temp) + strlen((char*)(void*)Marshal::StringToHGlobalAnsi("/" + addressInfo->GetType()->ToString() + ".bin")) + 1];
		strcpy_s(directory, strlen("Version/") + 1, "Version/");
		strcat_s(directory, strlen("Version/") + strlen(temp) + 1, temp);
		strcat_s(directory, strlen("Version/") + strlen(temp) + strlen((char*)(void*)Marshal::StringToHGlobalAnsi("/" + addressInfo->GetType()->ToString() + ".bin")) + 1, (char*)(void*)Marshal::StringToHGlobalAnsi("/" + addressInfo->GetType()->ToString() + ".bin"));
		in->open(directory, std::ios::binary);

		if (in->is_open())
		{
			addressInfo->addressSize = Load<int>();
			addressInfo->address = Load(addressInfo->addressSize);
			addressInfo->offsetNum = Load<int>();
			addressInfo->ResizeOffset(addressInfo->offsetNum);
			for (int n = 0; n < addressInfo->offsetNum; n++)
				addressInfo->SetOffset(Load<DWORD>(), n);
			in->close();
		}
	}

	generic <typename classType>
	void AddressHandler<classType>::Save(char * temp, int size)
	{
		out->write((const char*)temp, size);
	}

	generic <typename classType>
	void AddressHandler<classType>::Save(INT64 temp, int size)
	{
		out->write((const char*)&temp, size);
	}

	generic <typename classType>
	generic <typename T>
	T AddressHandler<classType>::Load()
	{
		T temp;
		in->read((char*)&temp, sizeof(T));
		return temp;
	}

	generic <typename classType>
	wchar_t * AddressHandler<classType>::Load(int size)
	{
			wchar_t * temp2 = new wchar_t[size];
			in->read((char*)temp2, size * 2);

			return temp2;
	}

	void Address::Copy(Address ^ temp)
	{
		this->addressSize = temp->addressSize;
		this->address = temp->address;
		this->offsetNum = temp->offsetNum;
		this->_offset.resize(this->offsetNum);
		for (int n = 0; n < this->offsetNum; n++)
			this->SetOffset(temp->GetOffset(n), n);
	}

	UnitAddressHandler::UnitAddressHandler()
	{
		_indexList = gcnew cliext::vector<UnitAddressInfo^>();
	}

	UnitAddressInfo ^ UnitAddressHandler::indexList(int index)
	{
		return _indexList[index];
	}

	void UnitAddressHandler::Copy(UnitAddressHandler ^ temp)
	{
		this->addressInfo->Copy(temp->addressInfo);
		this->addressInfo->pointerAddress = temp->addressInfo->pointerAddress;
		this->addressInfo->checkAddress = temp->addressInfo->checkAddress;
		this->_indexList.resize(temp->_indexList.size());

		for (int n = 0; n < this->_indexList.size(); n++)
		{
			this->_indexList[n]->name = temp->_indexList[n]->name;
			this->_indexList[n]->grade = temp->_indexList[n]->grade;
			this->_indexList[n]->unit = temp->_indexList[n]->unit;
			this->_indexList[n]->address = temp->_indexList[n]->address;
			this->_indexList[n]->num = temp->_indexList[n]->num;
			this->_indexList[n]->index = temp->_indexList[n]->index;
		}
	}

	void UnitAddressHandler::SearchAddress(UnitHandler ^ uh, ProcessHandler ^ ph)
	{
		DWORD temp;
		DWORD value = 0;
		DWORD tempPointer = 0;

		if (!addressInfo->check)
		{
			DWORD BaseAddress = (DWORD)ph->GetModuleBase(addressInfo->address, ph->process->processId);

			for (int n = 0; n < addressInfo->offsetNum - 1; n++)
			{
				ReadProcessMemory(ph->process->processH, (char*)BaseAddress + addressInfo->GetOffset(n), &BaseAddress, sizeof(DWORD), NULL);
			}

			ReadProcessMemory(ph->process->processH, (char*)BaseAddress + addressInfo->GetOffset(addressInfo->offsetNum - 1)
				, &tempPointer, sizeof(DWORD), NULL);

			addressInfo->pointerAddress = tempPointer + 0x10;
			addressInfo->checkAddress = addressInfo->pointerAddress;
			addressInfo->checkAddress -= addressInfo->checkAddress % 0x10000;
			addressInfo->check = true;
		}



		while (1)
		{
			ReadProcessMemory(ph->process->processH, (char*)addressInfo->pointerAddress, &temp, sizeof(temp), NULL);

			if (temp - (temp % 0x10000) == addressInfo->checkAddress)
			{
				ReadProcessMemory(ph->process->processH, (char*)temp + 0x14, &value, sizeof(value), NULL);
				for (int n = 0; n < uh->gradeNum; n++)
				{
					for (int i = 0; i < uh->grade(n)->unitNum; i++)
					{
						if (value == uh->grade(n)->unit(i)->index)
							uh->grade(n)->unit(i)->address = temp + 0x28;	
					}
				}
				DWORD temp2;
				ReadProcessMemory(ph->process->processH, (char*)temp + 0x10, &temp2, sizeof(DWORD), NULL);
				if (temp2 - (temp2 % 0x10000) == addressInfo->checkAddress)
					addressInfo->pointerAddress = temp + 0x10;
				else
					break;
			}
			else
				break;
		}
	}

	void UnitAddressHandler::SearchList(UnitHandler ^ uh, ProcessHandler ^ ph)
	{
		DWORD temp;
		DWORD value = 0;
		DWORD tempPointer = 0;

		DWORD BaseAddress = (DWORD)ph->GetModuleBase(addressInfo->address, ph->process->processId);

		for (int n = 0; n < addressInfo->offsetNum - 1; n++)
		{
			ReadProcessMemory(ph->process->processH, (char*)BaseAddress + addressInfo->GetOffset(n), &BaseAddress, sizeof(DWORD), NULL);
		}

		ReadProcessMemory(ph->process->processH, (char*)BaseAddress + addressInfo->GetOffset(addressInfo->offsetNum - 1)
			, &tempPointer, sizeof(DWORD), NULL);

		DWORD index = 0;
		DWORD address1 = 0x5188d44;
			for (int i = 0; i < 300; i++) {
				ReadProcessMemory(ph->process->processH, (char*)address1 + 0x1c * i
					, &index, sizeof(index), NULL);
				_indexList.push_back(gcnew UnitAddressInfo(address1 + 0x1c * i, index, 0, "", -1, -1));
			}
			indexListSize = 300;
		/*
		if (addressInfo->GetOffset(addressInfo->offsetNum - 1) == 0x10) {
			tempPointer += 0x10;
			if (!addressInfo->check)
			{
				addressInfo->checkAddress = tempPointer;
				addressInfo->checkAddress -= addressInfo->checkAddress % 0x10000;
				
			}
			
			int num = 0;
			bool check = false;
			int count = 0;

			while (1)
			{
				ReadProcessMemory(ph->process->processH, (char*)tempPointer, &temp, sizeof(temp), NULL);

				if (temp - (temp % 0x10000) == addressInfo->checkAddress)
				{
					ReadProcessMemory(ph->process->processH, (char*)temp + 0x14, &value, sizeof(value), NULL);
					ReadProcessMemory(ph->process->processH, (char*)temp + 0x14 + 0x14, &num, sizeof(int), NULL);
					for (int n = 0; n < uh->gradeNum; n++)
					{
						for (int i = 0; i < uh->grade(n)->unitNum; i++)
						{
							if (value == uh->grade(n)->unit(i)->index)
							{
								uh->grade(n)->unit(i)->address = temp + 0x28;
								check = true;
								if (count < _indexList.size())
									_indexList[count] = gcnew UnitAddressInfo(temp, value, num, uh->grade(n)->unit(i)->name, n, i);
								else
									_indexList.push_back(gcnew UnitAddressInfo(temp, value, num, uh->grade(n)->unit(i)->name, n, i));

								count++;
							}
						}
					}
					if (check == false)
					{
						if (count < _indexList.size())
							_indexList[count] = gcnew UnitAddressInfo(temp, value, num, "", -1, -1);
						else
							_indexList.push_back(gcnew UnitAddressInfo(temp, value, num, "", -1, -1));
						count++;
					}
					tempPointer = temp + 0x10;
					check = false;
				}
				else
				{
					indexListSize = count;
					break;
				}
			}
		}
		else if (addressInfo->GetOffset(addressInfo->offsetNum - 1) == 0xc) {
			tempPointer;
			if (!addressInfo->check)
			{
				addressInfo->checkAddress = tempPointer;
				addressInfo->checkAddress -= addressInfo->checkAddress % 0x10000;
			}

			int num = 0;
			bool check = false;
			int count = 0;

			while (1)
			{
				ReadProcessMemory(ph->process->processH, (char*)tempPointer, &temp, sizeof(temp), NULL);

				if (temp - (temp % 0x10000) == addressInfo->checkAddress)
				{
					ReadProcessMemory(ph->process->processH, (char*)temp + 0x8, &value, sizeof(value), NULL);
					ReadProcessMemory(ph->process->processH, (char*)temp + 0x10, &num, sizeof(int), NULL);

					if (check == false)
					{
						if (count < _indexList.size())
							_indexList[count] = gcnew UnitAddressInfo(temp, value, num, "", -1, -1);
						else
							_indexList.push_back(gcnew UnitAddressInfo(temp, value, num, "", -1, -1));
						count++;
					}
					tempPointer = temp;
					check = false;
				}
				else
				{
					indexListSize = count;
					break;
				}
			}
		}
		*/
	}

	UnitAddressInfo::UnitAddressInfo(DWORD address, DWORD index, int num, char * name, int grade, int unit)
	{
		this->address = address;
		this->grade = grade;
		this->unit = unit;
		this->index = index;
		this->num = num;
		this->name = name;
	}

	void GoldAddressHandler::SearchAddress(ResourcesHandler ^ rh, ProcessHandler ^ ph)
	{
		DWORD BaseAddress = (DWORD)ph->GetModuleBase(addressInfo->address, ph->process->processId);

		for (int n = 0; n < addressInfo->offsetNum - 1; n++)
		{
			ReadProcessMemory(ph->process->processH, (char *)BaseAddress + addressInfo->GetOffset(n), &BaseAddress, sizeof(BaseAddress), NULL);
		}
		rh->resources->goldAddress = BaseAddress + addressInfo->offset[addressInfo->offsetNum - 1];
	}

	void TreeAddressHandler::SearchAddress(ResourcesHandler ^ rh, ProcessHandler ^ ph)
	{
		DWORD BaseAddress = (DWORD)ph->GetModuleBase(addressInfo->address, ph->process->processId);

		for (int n = 0; n < addressInfo->offsetNum - 1; n++)
		{
			ReadProcessMemory(ph->process->processH, (char *)BaseAddress + addressInfo->offset[n], &BaseAddress, sizeof(BaseAddress), NULL);
		}
		rh->resources->treeAddress = BaseAddress + addressInfo->offset[addressInfo->offsetNum - 1];
	}

	ResourcesAddressHandler::ResourcesAddressHandler()
	{
		gold = gcnew GoldAddressHandler();
		tree = gcnew TreeAddressHandler();
	}

	void ResourcesAddressHandler::Copy(ResourcesAddressHandler ^ temp)
	{
		this->gold->addressInfo->Copy(temp->gold->addressInfo);
		this->tree->addressInfo->Copy(temp->tree->addressInfo);
	}

	void GameStateAddressHandler::Copy(GameStateAddressHandler ^ temp)
	{
		this->addressInfo->Copy(temp->addressInfo);
	}

	void GameStateAddressHandler::SearchAddress(GameStateHandler ^ gh, ProcessHandler ^ ph)
	{
		DWORD BaseAddress = (DWORD)ph->GetModuleBase(addressInfo->address, ph->process->processId);

		for (int n = 0; n < addressInfo->offsetNum - 1; n++)
		{
			ReadProcessMemory(ph->process->processH, (char *)BaseAddress + addressInfo->offset[n], &BaseAddress, sizeof(BaseAddress), NULL);
		}
		gh->gameState->address = BaseAddress + addressInfo->offset[addressInfo->offsetNum - 1];
	}
}