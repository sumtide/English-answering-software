   M S B A M L   `   `   `  ����  test    test.QuestionView   5      e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          N� Title$  QuestionView��5      .��5	      � -   5        $��grid_animationLayout��5	      ��5
   
   
� 5       
� GradientOrigin$ 0.5,0&�6"    
� Center$ 0.5,0.3&�69    
� RadiusX$	 0.7Q�6J   .��5      �  � Color$ 	AliceBlue��6    � Offset$ 0Q�61   5       � RowDefinitions 5   
   �� 5      $
��125* �6   �� 5      $	��50* �6   �� 5      $	��20* �6   �� 5      $
��200* �6   �� 5      $
��100* �6   .+�5   
   � -   5        $��grid_lineContainer��5      �� 5   
   -   5        $��cvs_starContainer��5      � 5   
   $��3��6    5      �� 5      $
��150* �6    �� 5      $
��150* �6     � ColumnDefinitions 5      �� 5      $
��400* �6#   �� 5       $
��400* �6#   .+�5#      �� -   5        $��	SelectOne��5#   2   $��0��6   $��0��6"      CommonButtonStyle#���   6D   �� 5$      -   5        $��	SelectTwo��5$   2   $��0��6   $��1��6"   #���   6D   �� 5%      -   5        $��SelectThree��5%   2   $��1��6   $��0��6"   #���   6E   �� 5&      -   5        $��
SelectFour��5&   2   $��1��6   $��1��6"   #���   6D   5'      � 5)   
   $��0��6   .+�5*      �� -	   5        $��Question��5*        	TextPanel#���  6)   
�������6M   5+      � 5,   
   $��1��6   .+�5-      �� -
   5        $��Prompt��5-        PromptPanel#���  6(   
�������6N   5.      � 5/   
   $��4��6   .+�50      �� -   5        $
��Next��50      $��	下一题��6"   #���   6C   $	��300��6o   51      � 52   
   $��2��6   .+�53      � -   5        $��pbStatus��53       � Value$ 0Q�6*     ProgressBarStyle#���  65   54      55      56      