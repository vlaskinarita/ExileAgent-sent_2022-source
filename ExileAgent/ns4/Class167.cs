using System;
using ns36;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns4
{
	internal static class Class167
	{
		public unsafe static Enum12 smethod_0(string string_0, string string_1)
		{
			void* ptr = stackalloc byte[17];
			*(byte*)ptr = (string_0.Contains(Class167.getString_0(107463330)) ? 1 : 0);
			Enum12 result;
			if (*(sbyte*)ptr != 0)
			{
				if ((string_0.Contains(Class167.getString_0(107463353)) || string_0.Contains(Class167.getString_0(107463296))) && string_0.Contains(Class167.getString_0(107463291) + string_1))
				{
					result = Enum12.const_1;
				}
				else if ((string_0.Contains(Class167.getString_0(107463353)) || string_0.Contains(Class167.getString_0(107463296))) && string_0.Contains(Class167.getString_0(107463318)))
				{
					result = Enum12.const_23;
				}
				else if (string_0.ToLower().Contains(Class167.getString_0(107361630)) || string_0.ToLower().Contains(Class167.getString_0(107463269)) || string_0.ToLower().Contains(Class167.getString_0(107463260)))
				{
					result = Enum12.const_15;
				}
				else
				{
					result = Enum12.const_13;
				}
			}
			else
			{
				((byte*)ptr)[1] = (string_0.Contains(Class167.getString_0(107463287)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = Enum12.const_19;
				}
				else
				{
					((byte*)ptr)[2] = (string_0.Contains(Class167.getString_0(107463198)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						result = Enum12.const_20;
					}
					else
					{
						((byte*)ptr)[3] = (string_0.Contains(Class167.getString_0(107463681)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							result = Enum12.const_2;
						}
						else
						{
							((byte*)ptr)[4] = (string_0.Contains(Class167.getString_0(107463652)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								result = Enum12.const_3;
							}
							else if (string_0.Contains(Class167.getString_0(107463659)) || string_0.Contains(Class167.getString_0(107463606)) || string_0.Contains(Class167.getString_0(107463577)))
							{
								result = Enum12.const_4;
							}
							else
							{
								((byte*)ptr)[5] = (string_0.Contains(Class167.getString_0(107463520)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 5) != 0)
								{
									result = Enum12.const_5;
								}
								else
								{
									((byte*)ptr)[6] = (string_0.Contains(Class167.getString_0(107463451)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 6) != 0)
									{
										result = Enum12.const_6;
									}
									else
									{
										((byte*)ptr)[7] = (string_0.Contains(Class167.getString_0(107462878)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 7) != 0)
										{
											result = Enum12.const_7;
										}
										else
										{
											((byte*)ptr)[8] = (string_0.Contains(Class167.getString_0(107462853)) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 8) != 0)
											{
												result = Enum12.const_8;
											}
											else
											{
												((byte*)ptr)[9] = (string_0.Contains(Class167.getString_0(107462840)) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 9) != 0)
												{
													result = Enum12.const_9;
												}
												else
												{
													((byte*)ptr)[10] = (string_0.Contains(Class167.getString_0(107462787)) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 10) != 0)
													{
														result = Enum12.const_10;
													}
													else
													{
														((byte*)ptr)[11] = (string_0.Contains(Class167.getString_0(107462798)) ? 1 : 0);
														if (*(sbyte*)((byte*)ptr + 11) != 0)
														{
															result = Enum12.const_11;
														}
														else
														{
															((byte*)ptr)[12] = (string_0.Contains(Class167.getString_0(107462773)) ? 1 : 0);
															if (*(sbyte*)((byte*)ptr + 12) != 0)
															{
																result = Enum12.const_12;
															}
															else if (string_0.Contains(Class167.getString_0(107462740)) || string_0.Contains(Class167.getString_0(107463183)))
															{
																result = Enum12.const_14;
															}
															else
															{
																((byte*)ptr)[13] = (string_0.Contains(Class167.getString_0(107463118)) ? 1 : 0);
																if (*(sbyte*)((byte*)ptr + 13) != 0)
																{
																	result = Enum12.const_16;
																}
																else
																{
																	((byte*)ptr)[14] = (string_0.Contains(Class167.getString_0(107463097)) ? 1 : 0);
																	if (*(sbyte*)((byte*)ptr + 14) != 0)
																	{
																		result = Enum12.const_17;
																	}
																	else
																	{
																		((byte*)ptr)[15] = (string_0.Contains(Class167.getString_0(107463040)) ? 1 : 0);
																		if (*(sbyte*)((byte*)ptr + 15) != 0)
																		{
																			result = Enum12.const_18;
																		}
																		else
																		{
																			((byte*)ptr)[16] = (string_0.Contains(Class167.getString_0(107462991)) ? 1 : 0);
																			if (*(sbyte*)((byte*)ptr + 16) != 0)
																			{
																				result = Enum12.const_22;
																			}
																			else if (string_0.Contains(Class167.getString_0(107462410)) || string_0.Contains(Class167.getString_0(107462353)))
																			{
																				result = Enum12.const_21;
																			}
																			else
																			{
																				result = Enum12.const_0;
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		static Class167()
		{
			Strings.CreateGetStringDelegate(typeof(Class167));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
