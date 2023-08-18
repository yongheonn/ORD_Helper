#pragma once
#include <Windows.h>
#include "Error.h"
#include "ProcessInfo.h"

#define War3GameMenu 0x2a

//	Single

#define SingleMenu 0x2f
#define SingleCustomMap 0xa5
#define SingleLoading1 0xE
#define SingleLoading2 0x17
#define SingleWait 0x184
#define SingleSelectLevel 0x191
#define SingleGameStart 0x19f
#define SingleScoreBoard  0x41
#define SingleGameStart2 0x1a3

//	Lan

#define LanMenu 0x3f
#define LanSelectMap 0x40
#define LanReadyRoom 0x9d
#define LanLoading1 0xe
#define LanLoading2 0x17
#define LanWait 0x194
#define LanSelectLevel 0x1a1
#define LanGameStart 0x1af
#define LanGameStart3 0x1B3
//#define LanGameStart4 0x
#define LanScoreBoard 0x41


//	BattleNet

#define NetLoginMenu 0x6b
#define NetMenu 0xc5
#define NetReady 0x10d
#define NetSelectMap 0x10b
#define NetReadyRoom 0x9d
#define NetWait 0x194
#define NetSelectLevel 0x1a1
#define NetGameStart 0x1af
#define NetScoreBoard 0x41

namespace ORD_Helper_Data {
	public ref class GameState
	{
	private:
		DWORD _state;
		DWORD _address;

	public:
		GameState();

		property DWORD state {
			DWORD get() { return _state; }
			void set(DWORD value) {
				if (value > 0)
					_state = value;
				else
					ErrorMessage(5, "GameState state set 오류");	//eCode 5
			}
		};
		property DWORD address {
			DWORD get() { return _address; }
			void set(DWORD value) {
				if (value > 0)
					_address = value;
				else
					ErrorMessage(48, "GameState address set 오류");
			}
		};
	};

	public ref class GameStateHandler
	{
	public:
		GameStateHandler();
		GameState ^ gameState;
		void Copy(GameStateHandler ^ temp);
		void ResearchGameState(ProcessHandler ^ ph);
	};
}
