using System.Collections;
using System.Text;

namespace ShiftRegister {
	public static class Calculus {
		private static BitArray _inputFlow;
		private static BitArray _keyFlow;
		private static Encoding cp866 = Encoding.GetEncoding(866);

		//функция оборота битового массива, потому что по дефолту младшие биты в нашем понимании тут - старшие и vise versa
		private static BitArray Reverse(BitArray array)
		{
			int length = array.Length;
			int mid = (length / 2);
			for (int i = 0; i < mid; i++)
			{
				bool bit = array[i];
				array[i] = array[length - i - 1];
				array[length - i - 1] = bit;
			}

			return array;
		}
		//инициализация потока открытого текста 
		private static void InitInputFlow(string input) {
			byte[] temp = cp866.GetBytes(input);
			_inputFlow = new BitArray(temp);
			_inputFlow = Reverse(_inputFlow);
		}
		//инициализация потока ключей
		private static void InitKeyFlow() {
			_keyFlow = new BitArray(new byte[]{50});
			_keyFlow = Reverse(_keyFlow);
		}
		//сдвиг регистра
		private static void ShiftRegister() {
			bool temp = GenerateNewElement();
			for (int j = _keyFlow.Length - 1; j > 0; j--) {
				_keyFlow[j] = _keyFlow[j - 1];
			}
			_keyFlow[0] = temp;
		}
		//фильтрация для генерации нового элемента
		private static bool GenerateNewElement() {
			return _keyFlow[7] ^ _keyFlow[3] ^ _keyFlow[0];
		}
		//превращаем битовый массив в стринг
		private static string BitArrayToString(BitArray bitArray) {
			bitArray = Reverse(bitArray);
			string text = "";
			byte[] bytes = new byte[bitArray.Length/8];
			bitArray.CopyTo(bytes, 0);
			char[] ch = cp866.GetChars(bytes);
			for (int i = 0; i < ch.Length; i++) {
				text += ch[i];
			}
			
			return text;
		}

		public static string Encrypt(string input) {
			InitKeyFlow();
			InitInputFlow(input);
			BitArray outputArray = new BitArray(_inputFlow.Length);
			for (int i = 0; i < _inputFlow.Length; i++) {
				outputArray.Set(i, _inputFlow[i] ^ _keyFlow[7]);
				ShiftRegister();
			}

			return BitArrayToString(outputArray);
		}
	}
}