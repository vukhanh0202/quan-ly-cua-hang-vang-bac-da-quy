   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   )    #GemstonesBusinessSystem.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:GemstonesBusinessSystem     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   
   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   -   5        $��loginWindow��5   	      MaterialDesignBody#���   5
   
   $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignFont#���  5        LoginVM � DataContext# [ 5   	    � Title$ Đăng nhập��5      $	��750��6   $
��1250��6$    � 
ResizeMode$ NoResize=�61    � WindowStartupLocation$ CenterScreen=�6G   ��5      �� 5   
    �� ImageSource$ Images/Anh bia.jpg��6   .��5      �  � ColumnDefinitions 5   
   �� 5      $��2* �6   6,   �� 5      6!   .+�5   
   #  MaterialDesignThemes.Wpf.Card  -   5          Name$ 	LoginCard��5   -   $��40 70q�6   $��1��6@   $��Stretch=�6P   $��Stretch=�6l   ��5      �� 5      ��5      �� 5       	 �� EndPoint$	 0.5,1&�6&   
 � MappingMode$
 RelativeToBoundingBox=�67    �� 
StartPoint$ 0.5,0&�6[    �� Opacity$	 0.2Q�6n   .��5!      �  � Color$ White��6#    � Offset$ 0Q�61   � 5"      $ White��6#   $ 1Q�61   5#      .��5%      �  � 	Resources 5&       �� 
TargetType �� Property �� Value��%d   '�       �� 5'      " �6   .�5(      ��  � ��$ 10 10 10 10q�63   5)      5*      5+       � RowDefinitions 5,      �� 5-      $
��111* �6$   64   �� 5.      $	��76* �6$   63   �� 5/      $	��76* �6$   63   �� 50      $	��70* �6$   63   �� 51      $
��166* �6$   64   .+�53      � $��0��6   $��-20,10,-11.6,5.2q�6$   .+�54      (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ PrimaryDark=�55      $��16q�56         CornerRadius$ 3��57      +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�58      $��0q�59      $��61��6(   $��Arial)�64   $��14(�6G   $
��Bold"�6U   ��5:      �� 5;       �� Color$ 	#FF110000��6.   $
 0.25Q�6@   .��5=      ��  �� Orientation$ 
Horizontal=�5>   !   .+�5?      d�  ' #MaterialDesignHamburgerToggleButton#���  5@   &   $��20��5A   %   $��47��60   $��
0,-11,0,-7q�6<   $��Bottom=�6P   �� 5B      $��Center=�5C   %   $��Center=�5D   %   $��
25,-9,0,-9q�5F   %   $��Arial)�69    �� TextAlignment$ Center=�6L   $��31��6c   $	��245��6o   $��24(�6{   �� 6�   $�LOGIN��6�   6�   5G      5H      5I      � 5J      $��1��6   .+�5K      �� -   5         ��Name$ UserName��5K      $��0 9 0 0q�5L   ?   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tên đăng nhập��5M       % !MaterialDesignFloatingHintTextBox#��[ 5N      $��Arial)�5O   "   $��22(�65   $
��Bold"�6D   ���#212529��5L   "   �� *TenDangNhap+5O   V   5P      � 5Q      $��2��6   .+�5R      G� -   5         G�Name$ Password��5S      $ Mật khẩu��5T       ) %MaterialDesignFloatingHintPasswordBox#��[ 5U      $��22(�6\   $��Arial)�6j   ���#212529$
��Bold"�5V      .  (System.Windows.Interactivity.Interaction   Triggers 5W      /  )System.Windows.Interactivity.EventTrigger  5X         	EventName$ PasswordChanged��6-   .  (System.Windows.Interactivity.TriggerBase   Actions. 5Y   "   6	  0System.Windows.Interactivity.InvokeCommandAction	    	  Command  68   �� *PasswordChangedCommand+! 	  CommandParameter! 5Z   <   �� " �� ElementName$" Password��5[      5]      5^      � 5_      $��3��6   $��
30 30 30 0q�6$   $��2��68   .+�5`      �� ���#888888# �� 	IsDefault# . 
�������$ � ToolTip$$ Đăng nhập��5e   !   $��46��66   $	��Top=�6B   $��24(�6Z   $
��Bold"�6h   $��Arial)�6z   ��5`      �� *LoginCommand+��5a   !   �� $" loginWindow��.��5e   �   Đăng Nhập5g      5h      5r      5s      � 5u   
   -   5        % �Name$% Login��5u      $��0��6   .+�5v      � -   5        $% ShowContent��5v       5w      �� 5x      $
��119* �6$   64   �� 5y      $
��765* �6$   64   .+�5{      � $��0��6   $	��100��6$   $
��Left=�60   ��5|      �� 5}      $ Images/LogoKPHC.png��6%   5�      � 5�      $��1��6   .+�5�      �� $��DIAMOND��6!   $��15,120,15,250q�5�   !   $
��Bold"�68   $��WrapWithOverflow=�6J   
�������$ Depth4=�5�   5   $	��140(�5�   !   $ Center=�60   5�      5�      5�      5�      5�      