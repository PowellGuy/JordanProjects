   M S B A M L   `   `   `  ����H  DTagsformatter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TagsformatterTagsformatter  5   	       Tagsformatter.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Tagsformatter     5   	     � Title$  TagsFormatter��5   	   $	��450��6   $
��1000��6,   .��5
      � -   5         �Name$ MainWindowGrid��5
      
�������$
��1000��66   $	��450��6C   $
��Left=�6P   .+�5   
   �� -   5         ��Name$ DarkModeCheckbox��5      $��	Dark Mode��6-   $
��Left=�6A   $	��Top=�6\   6�   �� 5   
   -   5         ��Name$ TF_StackPanel��5      
�������$
��Left=�6?   $	��400��6Z   $��Center=�6g   $	��450��6�   $��	0,12,0,13q�6�   $��3��6�   .+�5      �� -   5         ��Name$ TF_TitleLabel��5      $��Tags Formatter��6+   $��Center=�6D   $��0,0,0,25q�6a   $��Center=�6s   $��	ObelixPro)�6�   $��25(�6�   �� 5      -   5        $ TF_HeadingLabel��5      $-��'Paste the text that you want formatting��6-   $��Center=�6_   $��0,0,0,0q�6|   $��Center=�6�   $��Tahoma)�6�   $��16(�6�   �� 5      $��1q�6   
���   �$	��191��6=   $	��304��6J   ._�5      �� -   5         ��Name$ 
TF_TextBox��5      $��Center=�6.   $��0,0,0,0q�6K    �� TextWrapping$
 Wrap=�6\   $��Center=�6p   $	��191��6�   $	��304��6�   
������� �� 
CaretBrush
 �����6�   5      �� 5      -   5         ��Name$ TF_ConvertButton��5      $��Convert��6/   $��Center=�6A   $��188,10q�6^   $	��Top=�6n   $��74��6�   
��� ���$��Tahoma)�6�   �� 5      -   5        $ TF_ContentStackPanel��5      	 �� Orientation$	 
Horizontal=�67   
��� ���.+�5      	� -	   5        
 	�Name$
 TF_YoutubeTags��5      $��Youtube��66   $��
20,10,0,10q�6H   $��Center=�6\   $��75��6y   $
��Kris)�6�   $��12(�6�   	� 5      -
   5        $
 TF_InstagramTags��5      $��	Instagram��68   $��
10,10,0,10q�6L   $��Center=�6a   $��75��6~   $��Lucida D)�6�   	� 5      -   5        $
 TF_TIKTOKTags��5      $��Tiktok��65   $��
10,10,0,10q�6F   $��Center=�6Z   $��75��6w   $��Tahoma)�6�   	� 5      -   5        $
 TF_FacebookTags��5      $��Facebook��67   $��10,10,10,10q�6J   $��Center=�6_   $��75��6|   $��Tahoma)�6�   	� 5      -   5        $
 TF_TwitterTags��5      $��Twitter��66   $��
10,10,0,10q�6H   $��Center=�6\   $��75��6y   $��Tahoma)�6�   5      5       �� 5"   
   -   5        $ App_StackPanel_Center��5"      
�������$��Center=�6G   $	��450��6d   $��Center=�6q   $��90��6�   �� 5$   
   -   5        $ YS_StackPanel��5$      
�������$��Right=�6?   $	��400��6[   $��Center=�6h   $	��450��6�   $��	0,12,0,13q�6�   $��3��6�   .+�5%      �� -   5        $ YS_TitleLabel��5%      $��YouTube Search��6+   $��Center=�6D   $��0,0,0,25q�6a   $��Center=�6s   $��	ObelixPro)�6�   $��25(�6�   �� 5&      -   5        $ YS_HeadingLabel��5&      $ ��Youtube Search Suggestions��6-   $��Center=�6R   $��0,0,0,0q�6o   $��Center=�6�   $��Tahoma)�6�   $��16(�6�   �� 5(      -   5         ��Name$ YS_ListView��5(      $��0,0,0,0q�6,   $��Center=�6=   $	��191��6X   $	��304��6e   
�������
���   �$��1q�6�    �� View 5*      �� 5+      .P�5-      �� $!��List of suggested searches.��6)    �� Width$	 304��6N   5/      54      57      5:      5=      