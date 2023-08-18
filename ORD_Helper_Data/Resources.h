#pragma once
#include "Error.h"
#include "ProcessInfo.h"
#include "GameState.h"
#include <Windows.h>

namespace ORD_Helper_Data
{
	public ref class Resources
	{
	private:
		int _gold;
		DWORD _goldAddress;
		int _tree;
		DWORD _treeAddress;

	public:
		Resources();

		property int gold
		{
			int get() { return _gold; }
			void set(int value) {
				if (value >= 0)
					_gold = value;
				else
					ErrorMessage(49, "Resources gold set 오류");
			}
		}

		property DWORD goldAddress
		{
			DWORD get() { return _goldAddress; }
			void set(DWORD value) {
				if (value >= 0)
					_goldAddress = value;
				else
					ErrorMessage(50, "Resources goldAddress set 오류");
			}
		}

		property int tree
		{
			int get() { return _tree; }
			void set(int value) {
				if (value >= 0)
					_tree = value;
				else
					ErrorMessage(51, "Resources tree set 오류");
			}
		}

		property DWORD treeAddress
		{
			DWORD get() { return _treeAddress; }
			void set(DWORD value) {
				if (value >= 0)
					_treeAddress = value;
				else
					ErrorMessage(52, "Resources treeAddress set 오류");
			}
		}
	};

	public ref class ResourcesHandler
	{
	private:
		std::ofstream *out;
		std::ifstream *in;
		
		template <typename T>
		void Save(T temp);

		template <typename T>
		T Load();
		char * Load(int size);
	public:
		ResourcesHandler();
		Resources ^ resources;

		void Copy(ResourcesHandler ^ temp);
		void SearchResources(ProcessHandler ^ ph, GameStateHandler ^ gh);
		void SaveResources();
		void LoadResources();
	};
}