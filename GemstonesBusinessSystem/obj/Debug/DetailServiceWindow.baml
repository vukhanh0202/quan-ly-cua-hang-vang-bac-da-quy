   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   1    +GemstonesBusinessSystem.DetailServiceWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   1local%clr-namespace:GemstonesBusinessSystem  5   	   -   5        $��detailService��5	   	     � WindowStartupLocation$  CenterScreen=�6      DetailServiceVM � DataContext# [  5
       � Title$ DetailServiceWindow��5   	   $	��480��6%   $	��800��62   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+   CommandParameter 5   $   ��  �� ElementName$ detailService��5      	 � 	Resources	 5      
 �� 
TargetType  tblTitle �� Property �� Value��%}   &	       �� 5   
   "
 ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 22(�6)   66   5      5      .��5      ]� ��. ���#2A323C.+�5   
   � $��60��6   $	��Top=�6   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6E   .+�5      �� $��Center=�6    �� Padding$ 20 5 0 0q�63   $��Chi tiết dịch vụ��6F   
�������$��25(�6q   $��DemiBold"�6   6�   5      � 5   
   $	��Top=�6   ���#2A323C.+�5      ]� ��.  $��0 0 0 15q�6/   .+�5      ��  �� CornerRadius$ 15��6   $��30 30 30 10q�6+   $	��Top=�6@   ���#323C48._�5       ]� $��20q�6!   .+�5!      �� $	��Top=�6$   $��Dịch vụ��69   
�������$��24(�6[   $��	20 20 0 0q�6i   $��Medium"�6|   6�   �� 5"      $	��Top=�6%    �� Orientation$ Vertical=�6:   $��20 10q�6Q   .+�5#      �� $ 
Horizontal=�6)   .+�5$   "   �� $��Tên dịch vụ ��6,   #��[ 6@   6d   �� 5%   "   $�� *��6,   
���  ��6I   5'      �� 5(      $��0 5 0 0q�6&   $��20(�67   
���������6X   �� *	TenDichVu+ �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�.��6�   5)      5*      ]� 5+      $	��Top=�6$   .+�5,      �� $
��Left=�6)   $ Vertical=�6?   $��20 10q�6V   .+�5-   "   �� $ 
Horizontal=�6-   .+�5.   &   �� $��
Đơn giá��60   #��[ 6?   6c   �� 5/   &   $�� *��60   
���  ��6M   50   #   �� 51   "   $��20(�6*   
�������$��0 5 0 10q�6�   $	��200��6�   $
��Left=�6�   ��6K   �� *DonGia+ �� StringFormat$ 	{0:#,##0}�� �� ConverterCulture$ vi-VN�� . $ PropertyChanged=�.��6  52   #   53      �� 54      $
��Left=�6)   $ Vertical=�6?   $��20 10q�6V   .+�55   "   �� $ 
Horizontal=�6-   .+�56   &   �� $��Mã dịch vụ��60   #��[ 6B   6f   �� 57   &   $�� *��60   
���  ��6M   58   #   �� 59   "    �� 
IsReadOnly . $��20(�6<   
�������$��0 5 0 10q�6�   $	��160��6�   $
��Left=�6�   ��6]   �� *
MaDichVu+ . $ PropertyChanged=�.��6�   5:   #   5;      5P      5Q      5R      �� 5S      $��
20 0 10 00q�6   $��Right=�61   $��Bottom=�6M   $ 
Horizontal=�6e   .+�5T      �� $��0 0 10 0q�6   $��Right=�60   $ 10��6G   $��45��6Y    5U        5V      $ MouseLeftButtonDown��6-   . 5W   "      68   �� *XacNhanCommand+ 6[   �� $ detailService��5X   #   5Y      ��5[      �� 5\      "
 ��6$   .�5]   "   ��  � �� �#04A2B36Q    �� Triggers 5^   "   P� 5_   &    P� Property �@J�IsMouseOver P� Value . . �5`   *   ��  � �� �#038B9A6Y   5a   '   5c      ._�5e      �� $ 	15 2 15 0q�6$   
�������$��Xác nhận��6L   $��Center=�6\   $��Center=�6w   $
��auto��5f   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5g      5h      �� 5i      $��0 0 20 0q�6   $��Right=�60   $ 10��6G   $��45��6Z    5j        5k      $ MouseLeftButtonDown��6-   . 5l   "      68   �� *HuyBoCommand+ 5m   8   �� $ detailService��5n   #   5o      ��5q      �� 5r      "
 ��6$   .�5s   "   ��  � �� �#F8F9FA6Q    5t   "   P� 5u   &    �@J�IsMouseOver . . �5v   *   ��  � �� �#E2E6EA6Y   5w   '   5y      ._�5{      �� $ 	15 2 15 0q�6$   ���#212529$��
Hủy bỏ��6N   $��Center=�6\   $��Center=�6w   $
��auto��5|   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5}      5~      5      5�      5�      5�      5�      