   M S B A M L   `   `   `  ���� G  CKeyGenerator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfExample1WpfExample1      WpfExample1.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:WpfExample1     -     �Name$  KeyGenerator�� � Title$ Key Generator��$	��388��$	��558��.���  � ColumnDefinitions �� $	��73* ��� $
��477* �.+��� -    ��Name$ 	MyButton1��$��Generate��$
��Left=�$��103,150,0,0q�$	��Top=�$��75��$��1�� J� RenderTransformOrigin$ -0.467,0.85&��� -    ��Name$ MyLabel1��$�� ��$
��Left=�$��
28,319,0,0q�$	��Top=�$	��270��$��1q�
���   �$��2���� -    ��Name$ textBox1��$
��Left=�$��23��$��
290,56,0,0q� �� TextWrapping$
 Wrap=�$��Write a Word or Sentence��$	��Top=�$	��177��{�.  $��1�� � Cursor$  ��$y�Hidden=��� -   	 ��Name$	 Numbers��$��Numbers��$
��Left=�$��	28,29,0,0q�$	��Top=�$��86��
 d� 	IsChecked$

 TrueY�$ 
0.5,-3.133&�$��2���� -   $	 Letters��$��Letters��$
��Left=�$��	28,67,0,0q�$	��Top=�$��2���� -   $	 Alphanumeric��$��AlphaNumeric��$
��Left=�$��
28,132,0,0q�$	��Top=�$��2��$ 0.031,1.067&�	� -    	�Name$ _8bits��$��64 Bytes��$
��Left=�$��
103,32,0,0q�$	��Top=�$

 TrueY�$ -1.021,0.467&�$��1��	� -	   $ _16bits��$��	128 Bytes��$
��Left=�$��
103,67,0,0q�$	��Top=�$��1���� -
   $	 
Uppercase2��$��	Uppercase��$
��Left=�$��
57,165,0,0q�$	��Top=�$��2���� -   $	 Symbols��$��Symbols��$
��Left=�$��
28,200,0,0q�$	��Top=�$��2���� -   $	 	randomKey��$��
Random Key��$
��Left=�$��
290,29,0,0q�$	��Top=�$ 0.479,0&�$

 TrueY�$��1���� -   $	 Mixtotal��$��MixTotal��$
��Left=�$��
28,245,0,0q�$	��Top=�$ 	0.239,0.2&�$��2���� -   $	 
Uppercase1��$��	Uppercase��$
��Left=�$��
57,100,0,0q�$	��Top=�$��2��	� -   $ _32bits��$��	256 Bytes��$
��Left=�$��103,100,0,0q�$	��Top=�$��1��$ 0.556,1.733&��� -   $
	 RNGC��$
��RNGC��$��1��$
��Left=�$��
193,29,0,0q�$	��Top=�$

 TrueY��� -   $	 SnowC��$��SnowC��$��1��$
��Left=�$��
193,67,0,0q�$	��Top=��� -   $ 	MyButton2��$��Save Key��$��1��$
��Left=�$��103,185,0,0q�$	��Top=�$��75���� -   $ textBox2��$��1��$
��Left=�$��88��$��248,148,0,0q�$
 Wrap=�$
��Site��$	��Top=�$��98���� -   $ 	MyButton3��$��	View Keys��$��1��$
��Left=�$��103,216,0,0q�$	��Top=�$��75���� -   $ textBox3��$��1��$
��Left=�$��86��$��368,148,0,0q�$
 Wrap=�$	��Key��$	��Top=�$��99���� -   $ textBox4��$��2��$
��Left=�$��23��$��
28,279,0,0q�$
 Wrap=�$��Site Key��$	��Top=�$	��120���� -   $ 	MyButton4��$��Export��$��1��$
��Left=�$��103,245,0,0q�$	��Top=�$��75��