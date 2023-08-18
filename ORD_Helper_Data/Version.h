#pragma once
#include <fstream>
#include <sstream>
#include <cliext/vector>

namespace ORD_Helper_Data
{
	public ref class Version
	{
		char * _verName;
	public:
		int verSize;
		property char * verName {
			char * get() { return _verName; }
			void set(char * value) {
				if (value != NULL)
				{
					verSize = strlen(value) + 1;
					_verName = new char[verSize];
					strcpy_s(_verName, verSize, value);
				}
			}
		}
	};

	public ref class VersionHandler
	{
		cliext::vector<Version^> _ver;
		std::ofstream *out;
		std::ifstream *in;

		template <typename T>
		void Save(T temp);

		template <typename T>
		T Load();
		char * Load(int size);
	public:
		VersionHandler();
		Version ^ ver(int index);
		int verNum;

		void ResizeVer(int size);
		void RemoveVer(int index);
		void SaveVerInfo();
		void LoadVerInfo();
	};
}