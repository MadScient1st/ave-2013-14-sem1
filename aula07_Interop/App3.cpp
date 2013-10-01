#using <mscorlib.dll>
#using "Ponto.dll"

using namespace System;

int main(){
	Ponto^ p = gcnew Ponto(5, 7);
	p->Print();
	Console::WriteLine("p._x = {0}\n", p->x);
}
