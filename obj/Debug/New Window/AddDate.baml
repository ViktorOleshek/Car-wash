   M S B A M L   `   `   `  ����B  >CarWash, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:CarWash.New_WindowCarWash.New_Window  5   	        CarWash.New_Window.AddDate   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   X TXceed.Wpf.Toolkit, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   ,local clr-namespace:CarWash.New_Window  5   	     � Title$  AddDate��5	   	   $	��130��6   $	��380��6&    � 	Resources 5
       �� 
TargetType �� Property �� Value��%=  '��       '��     �� 5   
   " ��6   .�5      ��  � ��$ 30��6'   �� 5       � ��$ 90��6&   �� 5       � ��$ 14(�6)   �� 5       � ��
 ��p��6+   �� 5       � ��$
 Left=�64   �� 5       � ��$	 Top=�62   5      �� 5   
   " ��6   .�5      ��  � ��
 ��p��6+   �� 5       � ��$ 30��6'   �� 5       � ��$ 90��6&   �� 5       � ��$
 Left=�64   �� 5       � ��$	 Top=�62   �� 5       � ��$
 Left=�6;   �� 5       � ��$	 Top=�69   5      5      .��5!      � .+�5"   
   �� $��Start time:��6   $��	10,10,0,0q�6&   &   Xceed.Wpf.Toolkit.DateTimeUpDown  5#   
   -   5          Name$ dateTimeUpDown_startTime��5#      $��
120,10,0,0q�6@   $��30��5$      $	��125��6      FormatString$ yyyy-MM-dd HH:mm��6'      Format$ Custom=�5%      $	��Top=�6   $
��Left=�67   �� 5'   
   $��	End time:��6   $��	10,50,0,0q�6$     5(   
   -   5        $ dateTimeUpDown_endTime��5(      $��
120,50,0,0q�6>   $��30��5)      $	��125��6   $ yyyy-MM-dd HH:mm��6'   $ Custom=�5*      $	��Top=�6   $
��Left=�67   �� 5,   
   -   5        $��	Save data��5,      $��
260,50,0,0q�6%   69   5-      5.      